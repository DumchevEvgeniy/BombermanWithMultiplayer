using System.Collections.Generic;

public class RandomPlacementOnEmptyPosition : RandomPlacement {
    protected override IEnumerable<CellWithGameObjects> GetNeededCells(Field field) {
        return field.GetEmptyCells();
    }
}