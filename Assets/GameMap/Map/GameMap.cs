using System;

public class GameMap : MapWithFloor {
    public GameMap(Int32 length, Int32 width, DynamicGameObject gameFloorTemplate, DynamicGameObject concreteCubeTemplate) 
        : base(length, width, gameFloorTemplate) {
        InitializeConcreteCubes(concreteCubeTemplate);
    }

    private void InitializeConcreteCubes(DynamicGameObject concreteCubeTemplate) {
        foreach(var cell in Field)
            if(CanPutConcreteCube(cell))
                cell.AddGameElement(concreteCubeTemplate);
    }

    private Boolean CanPutConcreteCube(CellWithGameObjects cell) {
        return OnLeftOrRightBorder(cell.IndexColumn) ||
                OnTopOrBottomBorder(cell.IndexRow) ||
                OnEvenPosition(cell.IndexRow, cell.IndexColumn);
    }
    private Boolean OnLeftOrRightBorder(Int32 indexColumn) {
        return indexColumn == 0 || indexColumn == Length - 1;
    }
    private Boolean OnTopOrBottomBorder(Int32 indexRow) {
        return indexRow == 0 || indexRow == Width - 1;
    }
    private Boolean OnEvenPosition(Int32 indexRow, Int32 indexColumn) {
        return indexRow.IsEven() && indexColumn.IsEven();
    }
}
