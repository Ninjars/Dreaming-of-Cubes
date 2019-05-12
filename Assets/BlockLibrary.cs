using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BlockLibrary : MonoBehaviour {
    private Block[] blocks;

    void Start() {

        var allBlocks = GetComponentsInChildren<Block>();
        foreach (Transform t in transform) {
            t.gameObject.SetActive(false);
        }

        int i = 0;
        foreach (var block in blocks) {
            var newBlock = Instantiate(block.gameObject);
            newBlock.transform.position = transform.position + Vector3.right * i;
            newBlock.SetActive(true);
            i++;
        }
    }

    public Block[] getBlocks(Direction direction, Aspect aspect) {
        return blocks.Where(block => {
            switch (direction) {
                case Direction.FORWARD:
                    return block.backAspect.Equals(aspect);
                case Direction.BACKWARD:
                    return block.frontAspect.Equals(aspect);
                case Direction.LEFT:
                    return block.rightAspect.Equals(aspect);
                case Direction.RIGHT:
                    return block.leftAspect.Equals(aspect);
                case Direction.UPWARD:
                    return block.bottomAspect.Equals(aspect);
                case Direction.DOWNWARD:
                    return block.topAspect.Equals(aspect);
                default:
                    throw new ArgumentOutOfRangeException("Direction " + direction);
            }
        }).ToArray();
    }
}
