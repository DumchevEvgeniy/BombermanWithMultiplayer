using System;
using UnityEngine;

public class DirectedNode : Cell {
    public Vector3 Direction { get; set; }

    public DirectedNode(Int32 indexRow, Int32 indexColumn)
        : base(indexRow, indexColumn) {
        Direction = Vector3.zero;
    }
    public DirectedNode(Cell node) : this(node.IndexRow, node.IndexColumn) { }

    public Vector3 GetRelativeDirection(Cell node) {
        var xDirection = (IndexRow - node.IndexRow).Normalize();
        var zDirection = (IndexColumn - node.IndexColumn).Normalize();
        return new Vector3(xDirection, 0, zDirection);
    }
}

