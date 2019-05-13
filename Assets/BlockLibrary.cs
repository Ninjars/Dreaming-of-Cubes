using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BlockLibrary : MonoBehaviour {
    private Block[] blocks;

    void Start() {
        blocks = GetComponentsInChildren<Block>().SelectMany(block => {
            Block[] rotatedBlocks = new Block[4];
            rotatedBlocks[0] = block;

            var block1 = Instantiate(block);
            block1.setRotation(Block.Rotation.FRONT_IS_RIGHT);
            rotatedBlocks[1] = block1;
            block1.transform.parent = transform;
            
            var block2 = Instantiate(block);
            block2.setRotation(Block.Rotation.FRONT_IS_BACK);
            rotatedBlocks[2] = block2;
            block2.transform.parent = transform;
            
            var block3 = Instantiate(block);
            block3.setRotation(Block.Rotation.FRONT_IS_LEFT);
            rotatedBlocks[3] = block3;
            block3.transform.parent = transform;

            return rotatedBlocks;
        }).ToArray();

        foreach (Transform t in transform) {
            t.gameObject.SetActive(false);
        }
    }

    public Block[] getBlocks() {
        return blocks;
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
