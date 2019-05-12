using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Structure {

    private System.Random random;
    private Vector3 rootOffset;
    private readonly Dictionary<Vector3, Block> blocks = new Dictionary<Vector3, Block>();
    private HashSet<Vector3> activeSites = new HashSet<Vector3>();
    private HashSet<Vector3> blockedSites = new HashSet<Vector3>();

    public Structure(System.Random random, Block foundation) {
        this.random = random;
        rootOffset = foundation.transform.position;
        blocks[Vector3.zero] = foundation;

        foreach (Direction dir in DirectionHelper.GetDirections()) {
            var aspect = foundation.getAspect(dir);
            switch (aspect) {
                case Aspect.ACCESS:
                case Aspect.OPEN:
                    var targetSite = dirToVector(dir);
                    if (!blockedSites.Contains(targetSite)) {
                        activeSites.Add(targetSite);
                    }
                    break;
                case Aspect.WINDOW:
                    var blockedSite = dirToVector(dir);
                    blockedSites.Add(blockedSite);
                    activeSites.Remove(blockedSite);
                    break;
            }
        }
    }

    public void addBlock(Vector3 position, Block block) {
        // var directions = DirectionHelper.GetDirections().ToArray();
        // var direction = directions[random.Next(directions.Count())];
        Vector3 site = activeSites.ToList()[random.Next(activeSites.Count())];

        // TODO: find directions from site which need to have certain aspects
        // TODO: this searching logic should be done from outside this class, 
        // TODO: somewhere which has access to block library

        // TODO: add block should simply sanity check that the target site is active,
        // TODO: then update the structure internally
    }

    private Vector3 dirToVector(Direction direction) {
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