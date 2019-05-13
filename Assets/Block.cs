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

    private Rotation rotation = Rotation.NORMAL;

    public enum Rotation {
        NORMAL,
        FRONT_IS_RIGHT,
        FRONT_IS_BACK,
        FRONT_IS_LEFT
    }

    internal void setRotation(Rotation rotation) {
        this.rotation = rotation;
        var rotateAngles = 0;
        switch (rotation) {
            case Rotation.NORMAL:
                rotateAngles = 0;
                break;
            case Rotation.FRONT_IS_RIGHT:
                rotateAngles = 90;
                break;
            case Rotation.FRONT_IS_BACK:
                rotateAngles = 180;
                break;
            case Rotation.FRONT_IS_LEFT:
                rotateAngles = 270;
                break;
            default:
                throw new ArgumentOutOfRangeException("Rotation " + rotation);
        }
        transform.Rotate(Vector3.up, rotateAngles);
    }

    private BlockAspect getBlockAspect(Direction direction) {
        switch (direction) {
            case Direction.FORWARD:
                switch (rotation) {
                    case Rotation.NORMAL:
                        return frontAspect;
                    case Rotation.FRONT_IS_RIGHT:
                        return rightAspect;
                    case Rotation.FRONT_IS_BACK:
                        return backAspect;
                    case Rotation.FRONT_IS_LEFT:
                        return leftAspect;
                    default:
                        throw new ArgumentOutOfRangeException("Rotation " + rotation);
                }
            case Direction.BACKWARD:
                switch (rotation) {
                    case Rotation.NORMAL:
                        return backAspect;
                    case Rotation.FRONT_IS_RIGHT:
                        return leftAspect;
                    case Rotation.FRONT_IS_BACK:
                        return frontAspect;
                    case Rotation.FRONT_IS_LEFT:
                        return rightAspect;
                    default:
                        throw new ArgumentOutOfRangeException("Rotation " + rotation);
                }
            case Direction.LEFT:
                switch (rotation) {
                    case Rotation.NORMAL:
                        return leftAspect;
                    case Rotation.FRONT_IS_RIGHT:
                        return frontAspect;
                    case Rotation.FRONT_IS_BACK:
                        return rightAspect;
                    case Rotation.FRONT_IS_LEFT:
                        return backAspect;
                    default:
                        throw new ArgumentOutOfRangeException("Rotation " + rotation);
                }
            case Direction.RIGHT:
                switch (rotation) {
                    case Rotation.NORMAL:
                        return rightAspect;
                    case Rotation.FRONT_IS_RIGHT:
                        return backAspect;
                    case Rotation.FRONT_IS_BACK:
                        return leftAspect;
                    case Rotation.FRONT_IS_LEFT:
                        return frontAspect;
                    default:
                        throw new ArgumentOutOfRangeException("Rotation " + rotation);
                }
            case Direction.UPWARD:
                return topAspect;
            case Direction.DOWNWARD:
                return bottomAspect;
            default:
                throw new ArgumentOutOfRangeException("Direction " + direction);
        }
    }

    public Aspect getAspect(Direction direction) {
        return getBlockAspect(direction).aspect;
    }

    internal object getAccess(Direction direction) {
        return getBlockAspect(direction).accessPosition;
    }
}
