using System;
using UnityEngine;

public class CellMovementCalculator<T> : IPonderableByMovement<T> where T : DirectedNode {
    public Int32 GetDistance(T source, T next) {
        var distanceByX = Math.Abs(source.IndexRow - next.IndexRow);
        var distanceByZ = Math.Abs(source.IndexColumn - next.IndexColumn);
        return distanceByX + distanceByZ;
    }

    public Int32 GetDistanceToDestinition(T source, T destinition) {
        return GetDistance(source, destinition);
    }

    public Int32 GetTimeToRotate(T source, T next) {
        Vector3 angle = source.Direction + source.GetRelativeDirection(next);
        Int32 positiveX = Math.Abs((Int32)angle.x);
        Int32 positiveZ = Math.Abs((Int32)angle.z);
        return Math.Max(positiveX, positiveZ);
    }
}