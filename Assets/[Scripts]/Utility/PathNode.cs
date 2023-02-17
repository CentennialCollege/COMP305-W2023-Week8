using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathNode
{
    public Vector2 position;
    public PathNode next;
    public PathNode prev;

    // constructor
    public PathNode(Vector2 position, PathNode next, PathNode prev)
    {
        this.position = position;
        this.next = next;
        this.prev = prev;
    }
}
