# Hover Distance Limiter Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver)

Limits minimum and maximum hover distance.

## Explanation of the values:

**Minimum Hover Distance:** The minimum `HoverDistance` where input is sent. (When **Use ReportID Workaround** is enabled, ReportID is used.)

**Maximum Hover Distance:** The maximum `HoverDistance` where input is sent. (When **Use ReportID Workaround** is enabled, ReportID is used.)

**Use ReportID Workaround:** Uses `ReportID` to filter input instead of `HoverDistance` values. Many tablets do not send `HoverDistance` but will send general pen detection strength readings which can be used to limit hover distance.

<br>

## How to find HoverDistance or ReportID:
- Open tablet debugger by going to `Tablets > Tablet debugger...`
- Put your pen within detection range of your tablet
- The `Tablet Report` box will show `HoverDistance` or `ReportID` which is the raw value for each.

    (If a `HoverDistance` is not shown, your tablet is not compatible with the default filtering method. Enabling **Use ReportID Workaround** and using `ReportID` values can be used on some tablets which do not send `HoverDistance`.)
