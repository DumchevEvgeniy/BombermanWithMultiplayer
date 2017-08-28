using System;
using System.Linq;
using System.Collections.Generic;

public abstract class RandomPlacement : SelectivePlacement {
    protected override IEnumerable<CellWithGameObjects> GetPlacements(IEnumerable<CellWithGameObjects> availableCells, Int32 elementsCount) {
        var result = new List<CellWithGameObjects>();
        var listAvailableCells = availableCells.ToList();
        while(result.Count != elementsCount && !listAvailableCells.IsEmpty()) {
            var indexCell = UnityEngine.Random.Range(0, listAvailableCells.Count);
            result.Add(listAvailableCells[indexCell]);
            listAvailableCells.RemoveAt(indexCell);
        }
        return result;
    }
}