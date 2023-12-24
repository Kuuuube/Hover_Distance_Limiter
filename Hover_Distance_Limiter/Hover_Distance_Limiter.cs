using OpenTabletDriver;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;
using OpenTabletDriver.Plugin.Output;
using OpenTabletDriver.Plugin;
using OpenTabletDriver.Plugin.DependencyInjection;
using System;
using System.Numerics;

namespace Hover_Distance_Limiter;

[PluginName("Hover Distance Limiter")]
public class Hover_Distance_Limiter : IPositionedPipelineElement<IDeviceReport>
{
    public IDeviceReport Hover_Distance(IDeviceReport input)
    {
        if (input is IProximityReport tabletReport) {
            if (tabletReport.HoverDistance < Hover_min | tabletReport.HoverDistance > Hover_max) {
                return null;
            }
        }
        return input;
    }

    public IDeviceReport Near_Proximity(IDeviceReport input)
    {
        if (NearProximity && input is IProximityReport tabletReport) {
            if (tabletReport.NearProximity == false) {
                return null;
            }
        }
        return input;
    }

    public IDeviceReport Pressure_Cutoff(IDeviceReport input)
    {
        if (input is ITabletReport tabletReport) {
            if (tabletReport.Pressure < Pressure_min | tabletReport.Pressure > Pressure_max) {
                return null;
            }
        }
        return input;
    }

    public IDeviceReport Remove_Pen(IDeviceReport input)
    {
        if (Pen && input is IEraserReport tabletReport) {
            if (tabletReport.Eraser == false) {
                return null;
            }
        }
        return input;
    }

    public IDeviceReport Remove_Eraser(IDeviceReport input)
    {
        if (Eraser && input is IEraserReport tabletReport) {
            if (tabletReport.Eraser == true) {
                return null;
            }
        }
        return input;
    }

    public event Action<IDeviceReport> Emit;

    public void Consume(IDeviceReport value)
    {
        if (output_mode_type == OutputModeType.unknown) {
            try_resolve_output_mode();
        }
        var report = Remove_Eraser(Remove_Pen(Hover_Distance(Near_Proximity(Pressure_Cutoff(value)))));

        if (report == null && output_mode_type == OutputModeType.relative && value is ITabletReport relative_report) {
            relative_report.Position = new Vector2(0, 0);
            Emit?.Invoke(relative_report);
            return;
        }

        Emit?.Invoke(report);
    }

    public PipelinePosition Position => PipelinePosition.PostTransform;

    [Property("Minimum Hover Distance"), DefaultPropertyValue(0), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Minimum Hover Distance: The minimum HoverDistance where input is sent.\n\n" +
        "(HoverDistance can be found in the tablet debugger for supported tablets.)")]
    public int Hover_min { set; get; }

    [Property("Maximum Hover Distance"), DefaultPropertyValue(255), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Maximum Hover Distance: The maximum HoverDistance where input is sent.\n\n" +
        "(HoverDistance can be found in the tablet debugger for supported tablets.)")]
    public int Hover_max { set; get; }

    [BooleanProperty("Use Near Proximity Cutoff", ""), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Use Near Proximity Cutoff: Uses NearProximity flag in Wacom tablet reports to filter out the unstable far range of hover where NearProximity is False.\n\n" +
        "(NearProximity can be found in the tablet debugger for supported tablets.)")]
    public bool NearProximity { set; get; }

    [Property("Minimum Pressure"), DefaultPropertyValue(0), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Minimum Pressure: The minimum Pressure where input is sent.\n\n" +
        "(Only used when Use Pressure Range Cutoff is enabled.)\n" +
        "(Pressure can be found in the tablet debugger.)")]
    public int Pressure_min { set; get; }

    [Property("Maximum Pressure"), DefaultPropertyValue(16384), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Maximum Pressure: The maximum Pressure where input is sent.\n\n" +
        "(Only used when Use Pressure Range Cutoff is enabled.)\n" +
        "(Pressure can be found in the tablet debugger.)")]
    public int Pressure_max { set; get; }

    [BooleanProperty("Remove Pen Reports", ""), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Remove Pen Reports: Uses Eraser flag to filter out reports where Eraser is False.\n\n" +
        "(Eraser can be found in the tablet debugger for supported tablets.)")]
    public bool Pen { set; get; }

    [BooleanProperty("Remove Eraser Reports", ""), ToolTip
        ("Hover Distance Limiter:\n\n" +
        "Remove Eraser Reports: Uses Eraser flag to filter out reports where Eraser is True.\n\n" +
        "(Eraser can be found in the tablet debugger for supported tablets.)")]
    public bool Eraser { set; get; }

    [Resolved]
    public IDriver driver;
    private OutputModeType output_mode_type = OutputModeType.unknown;
    private void try_resolve_output_mode()
    {
        if (driver is Driver drv) {
            IOutputMode output = drv.InputDevices.Where(dev => dev?.OutputMode?.Elements?.Contains(this) ?? false).Select(dev => dev?.OutputMode).FirstOrDefault();

            if (output is AbsoluteOutputMode abs_output) {
                output_mode_type = OutputModeType.absolute;
                return;
            }
            if (output is RelativeOutputMode rel_output) {
                output_mode_type = OutputModeType.relative;
                return;
            }
            output_mode_type = OutputModeType.unknown;
        }
    }
}

enum OutputModeType {
    absolute,
    relative,
    unknown
}