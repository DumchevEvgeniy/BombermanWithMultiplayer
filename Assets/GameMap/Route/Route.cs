using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Route : IEnumerable<Cell> {
    public Cell FirstCell { get; protected set; }
    public Cell SecondCell { get; protected set; }
    protected Field field;

    public Route(Cell firstCell, Cell secondCell, Field field) {
        FirstCell = firstCell;
        SecondCell = secondCell;
        this.field = field;
    }

    public Boolean Exist(Cell cell) {
        return this.Any(el => el.Equals(cell));
    }

    public Boolean IsHorizontal() {
        return FirstCell.IndexRow == SecondCell.IndexRow;
    }
    public Boolean IsVertical() {
        return FirstCell.IndexColumn == SecondCell.IndexColumn;
    }

    public IEnumerator<Cell> GetEnumerator() {
        if(IsVertical()) {
            var from = Math.Min(FirstCell.IndexRow, SecondCell.IndexRow);
            var to = Math.Max(FirstCell.IndexRow, SecondCell.IndexRow);
            for(Int32 indexRow = from; indexRow <= to; indexRow++)
                yield return field.GetCell(indexRow, FirstCell.IndexColumn);
        }
        else {
            var from = Math.Min(FirstCell.IndexColumn, SecondCell.IndexColumn);
            var to = Math.Max(FirstCell.IndexColumn, SecondCell.IndexColumn);
            for(Int32 indexColumn = from; indexColumn <= to; indexColumn++)
                yield return field.GetCell(FirstCell.IndexRow, indexColumn);
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
