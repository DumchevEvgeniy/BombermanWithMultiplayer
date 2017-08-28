using System;

public interface IPonderableByMovement<T> where T : class {
    Int32 GetDistanceToDestinition(T source, T destinition);
    Int32 GetDistance(T source, T next);
    Int32 GetTimeToRotate(T source, T next);
}