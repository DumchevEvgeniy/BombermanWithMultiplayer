using System;
using System.Collections.Generic;

public class RouteWithBarrier : Route {
    private List<Type> barrierTypes = new List<Type>();
    public List<Type> BarrierTypes { get { return barrierTypes; } }

    public RouteWithBarrier(Cell firstCell, Cell secondCell, Field field)
        : base(firstCell, secondCell, field) {
    }

    public Boolean ExistBarrier() {
        foreach(var cell in this) {
            var currentCell = field.GetCell(cell.IndexRow, cell.IndexColumn);
            if(currentCell.IsEmpty())
                continue;
            if(currentCell.DynamicGameObjects.Exists(el => barrierTypes.Exists(t => el.IsDerived(t))))
                return true;
        }
        return false;
    }
}