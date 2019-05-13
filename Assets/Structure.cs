using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Structure : MonoBehaviour {

    private readonly Dictionary<Vector3, Block> blocks = new Dictionary<Vector3, Block>();
    private HashSet<Vector3> activeSites = new HashSet<Vector3>();
    private Vector3 minExtent;
    private Vector3 maxExtent;

    public Vector3[] getPotentialPositions() {
        return activeSites.ToArray();
    }

    public void addBlock(Vector3 position, Block blockPrefab) {
        var block = Instantiate(blockPrefab);
        block.gameObject.SetActive(true);
        block.transform.parent = transform;
        block.transform.position = transform.position + position;

        activeSites.Remove(position);
        blocks[position] = block;

        foreach (Direction dir in DirectionHelper.GetDirections()) {
            var aspect = block.getAspect(dir);
            Vector3 targetSite = DirectionHelper.dirToVector(dir) + position;
            if (blocks.ContainsKey(targetSite)) continue;

            switch (aspect) {
                case Aspect.ACCESS:
                case Aspect.OPEN:
                    Debug.Log("Adding active site " + dir + " from " + position + ": " + targetSite);
                    activeSites.Add(targetSite);
                    break;
            }
        }

        minExtent = new Vector3(Mathf.Min(position.x-0.5f, minExtent.x), Mathf.Min(position.y-0.5f, minExtent.y), Mathf.Min(position.z-0.5f, minExtent.z));
        maxExtent = new Vector3(Mathf.Max(position.x+0.5f, maxExtent.x), Mathf.Max(position.y+0.5f, maxExtent.y), Mathf.Max(position.z+0.5f, maxExtent.z));
    }

    public Bounds getBounds() {
        Vector3 halfSize = (maxExtent - minExtent) / 2f;
        Vector3 center = minExtent + halfSize;
        return new Bounds(center, halfSize);
    }

    public bool testBlock(Vector3 position, Block block) {
        if (!activeSites.Contains(position)) return false;
        
        foreach (Direction dir in DirectionHelper.GetDirections()) {
            Block other = null;
            blocks.TryGetValue(DirectionHelper.dirToVector(dir) + position, out other);
            if (other != null) {
                bool aspectsAreCompatible = getMatchingAspects(block, other, dir);
                if (!aspectsAreCompatible) return false;
            }
        }

        return true;
    }

    private bool getMatchingAspects(Block fromBlock, Block toBlock, Direction dir) {
        var aspectA = fromBlock.getAspect(dir);
        var aspectB = toBlock.getAspect(DirectionHelper.GetInverse(dir));

        switch (aspectB) {
            case Aspect.WINDOW:
                return aspectA == Aspect.OPEN;
            case Aspect.ACCESS:
                return aspectA == Aspect.OPEN || aspectA == Aspect.ACCESS && fromBlock.getAccess(dir) == toBlock.getAccess(DirectionHelper.GetInverse(dir));
            case Aspect.OPEN:
                return aspectA == Aspect.OPEN || aspectA == Aspect.WINDOW || aspectA == Aspect.ACCESS;
            case Aspect.CLOSED:
                return aspectA == Aspect.OPEN || aspectA == Aspect.CLOSED;
            default:
                throw new ArgumentOutOfRangeException("Aspect " + aspectA);
        }
    }
}
