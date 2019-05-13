using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Architect : MonoBehaviour {
    public int seed;
    public BlockLibrary blockLibrary;
    public Structure structurePrefab;

    private System.Random random;
    private List<Structure> archivedStructures;
    private Transform archive;
    private Structure activeStructure;

    private void Start() {
        if (seed == 0) random = new System.Random();
        else random = new System.Random(seed);

        archivedStructures = new List<Structure>();
        archive = new GameObject().transform;
        archive.position = transform.position + Vector3.forward * 12;
        archive.gameObject.name = "Archive";
    }

    public void beginNewStructure() {
        if (activeStructure != null) {
            activeStructure.transform.parent = archive;
            if (archivedStructures.Count > 0) {
                var previousArchive = archivedStructures.Last();
                var previousBounds = previousArchive.getBounds();
                var currentBounds = activeStructure.getBounds();
                Debug.Log("previous bounds " + previousBounds.size);
                Debug.Log("current  bounds " + currentBounds.size);
                activeStructure.transform.position = previousArchive.transform.position + Vector3.right * (previousBounds.size.x + currentBounds.size.x);

            } else {
                activeStructure.transform.position = archive.position;
            }
            archivedStructures.Add(activeStructure);
        }

        activeStructure = Instantiate(structurePrefab);
        activeStructure.addBlock(Vector3.zero, getRandomBlock());
    }
    
    public void addBlockToActiveStructure() {
        tryAddBlockToActiveStructure();
    }

    public bool tryAddBlockToActiveStructure() {
        if (activeStructure == null) {
            beginNewStructure();
            return true;
        }

        var validPositions = activeStructure.getPotentialPositions();
        var positionCount = validPositions.Count();
        if (positionCount == 0) return false;

        Vector3 targetPosition = validPositions[random.Next(positionCount)];
        Block validBlock = blockLibrary.getBlocks()
                                        .OrderBy(x => random.Next())
                                        .Where(block => activeStructure.testBlock(targetPosition, block))
                                        .FirstOrDefault();
        if (validBlock != null) {
            activeStructure.addBlock(targetPosition, validBlock);
            return true;
        }
        return false;
    }

    private Block getRandomBlock() {
        var blocks = blockLibrary.getBlocks();
        return blocks[random.Next(blocks.Count())];
    }
}
