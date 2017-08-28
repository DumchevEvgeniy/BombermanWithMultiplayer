using System;

public class Cell {
    public Int32 IndexRow { get; private set; }
    public Int32 IndexColumn { get; private set; }

    public Cell(Int32 indexRow, Int32 indexColumn) {
        IndexRow = indexRow;
        IndexColumn = indexColumn;
    }

    public override Boolean Equals(Object obj) {
        if(obj == null)
            return false;
        var cell = obj as Cell;
        if(cell == null)
            return false;
        return IndexRow == cell.IndexRow && IndexColumn == cell.IndexColumn;
    }
    public override Int32 GetHashCode() {
        return IndexRow | IndexColumn;
    }
}
