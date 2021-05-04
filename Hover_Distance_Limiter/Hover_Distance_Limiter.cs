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
            if (!ReportID)
            {
                if (input is IProximityReport tabletReport)
                {
                    if (tabletReport.HoverDistance >= Hover_min && tabletReport.HoverDistance <= Hover_max)
                    {
                        return input;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return input;
                }
            }
            else
            {
                if (input is ITabletReport tabletReport)
                {
                    if (tabletReport.ReportID >= Hover_min && tabletReport.ReportID <= Hover_max)
                    {
                        return input;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return input;
                }
            }
        }

        public event Action<IDeviceReport> Emit;

        public void Consume(IDeviceReport value)
        {
            IDeviceReport report = Filter(value);
            value = report;

            Emit?.Invoke(value);
        }

        public IDeviceReport Filter(IDeviceReport input) => Hover_Distance(input);

        public PipelinePosition Position => PipelinePosition.PreTransform;

        [Property("Minimum Hover Distance"), DefaultPropertyValue(0f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Minimum Hover Distance: The minimum HoverDistance where input is sent.\n" +
            "(When Use ReportID Workaround is enabled, ReportID is used.)\n\n" +
            "(HoverDistance and ReportID values can be found in the tablet debugger for supported tablets.)")]
        public float Hover_min { set; get; }

        [Property("Maximum Hover Distance"), DefaultPropertyValue(63f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Maximum Hover Distance: The maximum HoverDistance where input is sent.\n" +
            "(When Use ReportID Workaround is enabled, ReportID is used.)\n\n" +
            "(HoverDistance and ReportID values can be found in the tablet debugger for supported tablets.)")]
        public float Hover_max { set; get; }

        [BooleanProperty("Use ReportID Workaround", ""), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Use ReportID Workaround: Uses ReportID to filter input instead of HoverDistance values.\n\n" +
            "Many tablets do not send HoverDistance but will send general pen detection strength readings which can be used to limit hover distance.\n\n" +
            "(ReportID values can be found in the tablet debugger)")]
        public bool ReportID { set; get; }
    }
}