using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public BlockAspect frontAspect;
    public BlockAspect backAspect;
    public BlockAspect leftAspect;
    public BlockAspect rightAspect;
    public BlockAspect topAspect;
    public BlockAspect bottomAspect;
    
    public Aspect getAspect(Direction direction) {
        switch (direction) {
            case Direction.FORWARD:
                    return frontAspect.aspect;
                case Direction.BACKWARD:
                    return backAspect.aspect;
                case Direction.LEFT:
                    return leftAspect.aspect;
                case Direction.RIGHT:
                    return rightAspect.aspect;
                case Direction.UPWARD:
                    return topAspect.aspect;
                case Direction.DOWNWARD:
                    return bottomAspect.aspect;
                default:
                    throw new ArgumentOutOfRangeException("Direction " + direction);
        }
    }

    internal object getAccess(Direction direction) {
        switch (direction) {
            case Direction.FORWARD:
                    return frontAspect.accessPosition;
                case Direction.BACKWARD:
                    return backAspect.accessPosition;
                case Direction.LEFT:
                    return leftAspect.accessPosition;
                case Direction.RIGHT:
                    return rightAspect.accessPosition;
                case Direction.UPWARD:
                    return topAspect.accessPosition;
                case Direction.DOWNWARD:
                    return bottomAspect.accessPosition;
                default:
                    throw new ArgumentOutOfRangeException("Direction " + direction);
        }
    }
}
