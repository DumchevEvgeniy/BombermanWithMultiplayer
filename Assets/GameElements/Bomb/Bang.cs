using System;
using UnityEngine;

public class Bang : DynamicGameObject {
    private Cell cell;

    public Bang(Int32 row, Int32 column) {
        cell = new Cell(row, column);
    }
    public Bang(Cell cell) :this(cell.IndexRow, cell.IndexColumn) { }

    protected override void InitializeSettings(GameObject bang) {
        bang.SetPosition(cell.IndexRow, cell.IndexColumn);
    }

    protected override String GetPrefabName() {
        return "Prefabs/Bomb/Bang";
    }
}