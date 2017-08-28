using System;
using System.Collections.Generic;

public class GameElements<T> where T : BasePlacement, new() {
    public DynamicGameObject Element { get; private set; }
    private Int32 elementsCount;

    public GameElements(DynamicGameObject element, Int32 elementsCount) {
        Element = element;
        this.elementsCount = elementsCount;
    }

    public IEnumerable<CellWithGameObjects> GetPlacements(Field field) {
        return new T().GetPlacements(field, elementsCount);
    }
}