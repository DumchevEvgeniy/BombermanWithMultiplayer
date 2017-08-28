using System;
using System.Collections.Generic;

public abstract class BasePlacement {
    public abstract IEnumerable<CellWithGameObjects> GetPlacements(Field field, Int32 elementsCount);
}