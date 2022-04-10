using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;
using OpenTabletDriver.Plugin.Output;
using System;

namespace Hover_Distance_Limiter
{
    [PluginName("Hover Distance Limiter")]
    public class Hover_Distance_Limiter : IPositionedPipelineElement<IDeviceReport>
    {
        public IDeviceReport Hover_Distance(IDeviceReport input)
        {
            if (input is IProximityReport tabletReport)
                if (tabletReport.HoverDistance < Hover_min | tabletReport.HoverDistance > Hover_max)
                    return null;
            return input;
        }

        public IDeviceReport Near_Proximity(IDeviceReport input)
        {
            if (NearProximity && input is IProximityReport tabletReport)
                if (tabletReport.NearProximity == false)
                    return null;
            return input;
        }

        public IDeviceReport Pressure_Cutoff(IDeviceReport input)
        {
            if (Pressure && input is ITabletReport tabletReport)
                if (tabletReport.Pressure < Pressure_min | tabletReport.Pressure > Pressure_max)
                    return null;
            return input;
        }

        public event Action<IDeviceReport> Emit;

        public void Consume(IDeviceReport value)
        {
            var report = Pressure_Cutoff(value);
            report = Near_Proximity(report);
            report = Hover_Distance(report);

            Emit?.Invoke(report);
        }

        public PipelinePosition Position => PipelinePosition.PreTransform;

        [Property("Minimum Hover Distance"), DefaultPropertyValue(0f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Minimum Hover Distance: The minimum HoverDistance where input is sent.\n\n" +
            "(HoverDistance can be found in the tablet debugger for supported tablets.)")]
        public float Hover_min { set; get; }

        [Property("Maximum Hover Distance"), DefaultPropertyValue(255f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Maximum Hover Distance: The maximum HoverDistance where input is sent.\n\n" +
            "(HoverDistance can be found in the tablet debugger for supported tablets.)")]
        public float Hover_max { set; get; }

        [BooleanProperty("Use Near Proximity Cutoff", ""), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Use Near Proximity Cutoff: Uses NearProximity flag in Wacom tablet reports to filter out the unstable far range of hover where NearProximity is False.\n\n" +
            "(NearProximity can be found in the tablet debugger for supported tablets.)")]
        public bool NearProximity { set; get; }

        [BooleanProperty("Use Pressure Range Cutoff", ""), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Use Pressure Range Cutoff: Uses Pressure to filter input along with either HoverDistance or ReportID.\n\n" +
            "(Pressure can be found in the tablet debugger)")]
        public bool Pressure { set; get; }

        [Property("Minimum Pressure"), DefaultPropertyValue(0f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Minimum Pressure: The minimum Pressure where input is sent.\n\n" +
            "(Only used when Use Pressure Range Cutoff is enabled.)\n" +
            "(Pressure can be found in the tablet debugger.)")]
        public float Pressure_min { set; get; }

        [Property("Maximum Pressure"), DefaultPropertyValue(8192f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Maximum Pressure: The maximum Pressure where input is sent.\n\n" +
            "(Only used when Use Pressure Range Cutoff is enabled.)\n" +
            "(Pressure can be found in the tablet debugger.)")]
        public float Pressure_max { set; get; }
    }
}