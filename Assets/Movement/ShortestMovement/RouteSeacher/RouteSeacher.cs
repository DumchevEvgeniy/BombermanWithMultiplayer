using System;
using System.Collections.Generic;

public class RouteSeacher<T> : IRouteAvailable<T> where T : Cell {
    private IEnumerable<Type> barriers;
    private Field field;

    public RouteSeacher(IEnumerable<Type> barriers, Field field) {
        this.barriers = barriers;
        this.field = field;
    }

    public IEnumerable<T> SelectAvailableCells(T current, params T[] cellForRoute) {
        foreach(var cell in cellForRoute) {
            var route = new RouteWithBarrier(current, cell, field);
            route.BarrierTypes.AddRange(barriers);
            if(!route.ExistBarrier())
                yield return cell;
        }
    }
}