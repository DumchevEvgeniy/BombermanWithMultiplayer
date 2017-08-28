using System;
using System.Collections.Generic;
using System.Linq;

public abstract class SelectivePlacement : BasePlacement {
    public override IEnumerable<CellWithGameObjects> GetPlacements(Field field, Int32 elementsCount) {
        var result = new List<CellWithGameObjects>();
        var availableCells = GetAvailableCells(GetNeededCells(field), GetProhibitedForUsingCells(field));
        if(availableCells == null)
            return result;
        return GetPlacements(availableCells, elementsCount) ?? result;
    }

    protected abstract IEnumerable<CellWithGameObjects> GetPlacements(IEnumerable<CellWithGameObjects> possibleCells, Int32 elementsCount);
    protected abstract IEnumerable<CellWithGameObjects> GetNeededCells(Field field);
    protected virtual IEnumerable<CellWithGameObjects> GetProhibitedForUsingCells(Field field) {
        return null;
    }

    private IEnumerable<T> GetAvailableCells<T>(IEnumerable<T> neededCells, IEnumerable<T> prohibitedCells) 
        where T : Cell {
        if(prohibitedCells == null && neededCells == null)
            return null;
        return prohibitedCells == null ? neededCells : neededCells.Except(prohibitedCells);
    }
}