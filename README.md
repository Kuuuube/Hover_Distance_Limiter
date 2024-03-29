# Hover Distance Limiter Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver) [![](https://img.shields.io/github/downloads/Kuuuube/Hover_Distance_Limiter/total.svg)](https://github.com/Kuuuube/Hover_Distance_Limiter/releases/latest)

Limits minimum and maximum hover distance.

## Hover Distance Limiter:

**Minimum Hover Distance:** The minimum `HoverDistance` where input is sent.

**Maximum Hover Distance:** The maximum `HoverDistance` where input is sent.

**Use Near Proximity Cutoff:** Uses the `NearProximity` flag in Wacom tablet reports to filter out the range of hover where `NearProximity` is `False`.

**Minimum Pressure:** The minimum `Pressure` where input is sent.

**Maximum Pressure:** The maximum `Pressure` where input is sent.

**Remove Pen Reports:** Uses `Eraser` flag to filter out reports where `Eraser` is `False`.

**Remove Eraser Reports:** Uses `Eraser` flag to filter out reports where `Eraser` is `True`.

## How to find HoverDistance or Pressure:
- Open tablet debugger by going to `Tablets > Tablet debugger...`
- Put your pen within detection range of your tablet
- The `Tablet Report` box will show the current values for `HoverDistance` and `Pressure`.

    `HoverDistance` will not show on all tablets. If this is the case for your tablet, `HoverDistance` is not supported.
