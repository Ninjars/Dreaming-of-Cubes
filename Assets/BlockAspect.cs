using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BlockAspect {
    public Aspect aspect = Aspect.CLOSED;
    public AccessPosition accessPosition = AccessPosition.NA;
}

public enum Direction {
    FORWARD,
    BACKWARD,
    LEFT,
    RIGHT,
    UPWARD, 
    DOWNWARD
}

static class DirectionHelper {
    private static IEnumerable<Direction> directionsCache = Enum.GetValues(typeof(Direction)).Cast<Direction>();
    public static IEnumerable<Direction> GetDirections() {
        return directionsCache;
    }
}

public enum Aspect {
    CLOSED,
    OPEN,
    WINDOW,
    ACCESS
}

public enum AccessPosition {
    NA,
    TOP_LEFT,
    TOP_CENTER,
    TOP_RIGHT,
    MIDDLE_LEFT,
    MIDDLE_CENTER,
    MIDDLE_RIGHT,
    BOTTOM_LEFT,
    BOTTOM_CENTER,
    BOTTOM_RIGHT,
}