using System;
using System.Collections.Generic;
using System.Linq;

public class CellWithGameObjects : Cell {
    public List<DynamicGameObject> DynamicGameObjects { get; set; }

    public CellWithGameObjects(Int32 indexRow, Int32 indexColumn)
        : base(indexRow, indexColumn) {
        DynamicGameObjects = new List<DynamicGameObject>();
    }

    public Boolean IsEmpty() {
        return DynamicGameObjects.IsEmpty();
    }
    public void AddGameElement(DynamicGameObject dynamicElement) {
        DynamicGameObjects.Add(dynamicElement);
    }
    public void RemoveGameElement(Type dynamicElementType) {
        var element = DynamicGameObjects.FirstOrDefault(el => el.IsDerived(dynamicElementType));
        if(element != null)
            DynamicGameObjects.Remove(element);
    }
}
