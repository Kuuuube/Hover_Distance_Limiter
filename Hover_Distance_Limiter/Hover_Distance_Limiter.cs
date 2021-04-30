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

        public event Action<IDeviceReport> Emit;

        public void Consume(IDeviceReport value)
        {
            if (value is IProximityReport report)
            {
                report = (IProximityReport)Filter(report);
                value = report;
            }

            Emit?.Invoke(value);
        }

        public IDeviceReport Filter(IDeviceReport input) => Hover_Distance(input);

        public PipelinePosition Position => PipelinePosition.PostTransform;

        [Property("Minimum Hover Distance"), DefaultPropertyValue(0f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Minimum Hover Distance: The minimum raw hover distance where input will be sent.\n\n" +
            "(Raw hover distance values can be found in the tablet debugger for supported tablets.)")]
        public float Hover_min { set; get; }

        [Property("Maximum Hover Distance"), DefaultPropertyValue(63f), ToolTip
            ("Hover Distance Limiter:\n\n" +
            "Maximum Hover Distance: The maximum raw hover distance where input will be sent.\n\n" +
            "(Raw hover distance values can be found in the tablet debugger for supported tablets.)")]
        public float Hover_max { set; get; }
    }
}