using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Architect : MonoBehaviour {
    public int seed;
    public BlockLibrary blockLibrary;
    private System.Random random;

    private void Start() {
        random = new System.Random(seed);
    }

    private Block getRandomBlock(Direction direction, Aspect aspect) {
        var selectedBlocks = blockLibrary.getBlocks(direction, aspect);
        int count = selectedBlocks.Count(); 
        if (count == 0) return null;
        else return selectedBlocks[random.Next(count)];
    } 
}
