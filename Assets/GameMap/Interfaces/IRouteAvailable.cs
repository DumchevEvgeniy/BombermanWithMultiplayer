using System.Collections.Generic;

public interface IRouteAvailable<T> where T : Cell {
    IEnumerable<T> SelectAvailableCells(T current, params T[] cellForRoute);
}