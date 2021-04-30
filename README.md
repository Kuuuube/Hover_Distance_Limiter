# Hover Distance Limiter Plugin for [OpenTabletDriver](https://github.com/OpenTabletDriver/OpenTabletDriver)

Limits minimum and maximum hover distance.

## Explanation of the values:

**Minimum Hover Distance:** The minimum raw hover distance where input will be sent.

**Maximum Hover Distance:** The maximum raw hover distance where input will be sent.

<br>

## How to find raw hover distance:
- Open tablet debugger by going to `Tablets > Tablet debugger...`
- Put your pen within detection range of tablet
- The `Tablet Report` box will show `HoverDistance` which is the raw hover distance.

    (If a `HoverDistance` does not show in the debugger that means your tablet is not compatible.)