# Hover Distance Limiter Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver) [![](https://img.shields.io/github/downloads/Kuuuube/Hover_Distance_Limiter/total.svg)](https://github.com/Kuuuube/Hover_Distance_Limiter/releases/latest)

Limits minimum and maximum hover distance.

## Explanation of the values:

**Minimum Hover Distance:** The minimum `HoverDistance` where input is sent. (When **Use ReportID Workaround** is enabled, ReportID is used.)

**Maximum Hover Distance:** The maximum `HoverDistance` where input is sent. (When **Use ReportID Workaround** is enabled, ReportID is used.)

**Use ReportID Workaround:** Uses `ReportID` to filter input instead of `HoverDistance`. Many tablets do not send `HoverDistance` but will send general pen detection strength readings which can be used to limit hover distance.

**Use Near Proximity Cutoff:** Uses `NearProximity` flag in Wacom tablet reports to filter out the unstable far range of hover where `NearProximity` is `False`.

**Use Pressure Range Cutoff:** Uses `Pressure` to filter input along with either `HoverDistance` or `ReportID`.

**Minimum Pressure:** The minimum `Pressure` where input is sent. (Only used when **Use Pressure Range Cutoff** is enabled.)

**Maximum Pressure:** The Maximum `Pressure` where input is sent. (Only used when **Use Pressure Range Cutoff** is enabled.)

<br>

## How to find HoverDistance, ReportID or Pressure:
- Open tablet debugger by going to `Tablets > Tablet debugger...`
- Put your pen within detection range of your tablet
- The `Tablet Report` box will show `HoverDistance`, `ReportID` and `Pressure` which is the raw value for each.

    (If a `HoverDistance` is not shown, your tablet is not compatible with the default filtering method. Enabling **Use ReportID Workaround** and using `ReportID` can be used on some tablets which do not send `HoverDistance`.)
