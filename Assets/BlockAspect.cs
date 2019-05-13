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

    public static Direction GetInverse(Direction direction) {
        switch (direction) {
            case Direction.FORWARD:
                    return Direction.BACKWARD;
                case Direction.BACKWARD:
                    return Direction.FORWARD;
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.LEFT;
                case Direction.UPWARD:
                    return Direction.DOWNWARD;
                case Direction.DOWNWARD:
                    return Direction.UPWARD;
                default:
                    throw new ArgumentOutOfRangeException("Direction " + direction);
        }
    }
    
    public static Vector3 dirToVector(Direction direction) {
        switch (direction) {
            case Direction.FORWARD:
                    return Vector3.forward;
                case Direction.BACKWARD:
                    return Vector3.back;
                case Direction.LEFT:
                    return Vector3.left;
                case Direction.RIGHT:
                    return Vector3.right;
                case Direction.UPWARD:
                    return Vector3.up;
                case Direction.DOWNWARD:
                    return Vector3.down;
                default:
                    throw new ArgumentOutOfRangeException("Direction " + direction);
        }
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