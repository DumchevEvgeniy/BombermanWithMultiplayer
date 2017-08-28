using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class Field : IEnumerable<CellWithGameObjects> {
    private CellWithGameObjects[,] field;
    public Int32 Length { get; private set; }
    public Int32 Width { get; private set; }

    public Field(Int32 length, Int32 width) {
        Length = length;
        Width = width;
        field = new CellWithGameObjects[width, length];
        InitializeCells();
    }

    private void InitializeCells() {
        for(Int32 indexRow = 0; indexRow < Width; indexRow++)
            for(Int32 indexColumn = 0; indexColumn < Length; indexColumn++)
                field[indexRow, indexColumn] = new CellWithGameObjects(indexRow, indexColumn);
    }

    public void AddElement(Int32 indexRow, Int32 indexColumn, DynamicGameObject element) {
        GetCell(indexRow, indexColumn).AddGameElement(element);
    }
    public void AddElement(Cell cell, DynamicGameObject element) {
        AddElement(cell.IndexRow, cell.IndexColumn, element);
    }

    public void RemoveElement(Int32 indexRow, Int32 indexColumn, Type type) {
        GetCell(indexRow, indexColumn).RemoveGameElement(type);
    }
    public void RemoveElement(Cell cell, Type type) {
        GetCell(cell.IndexRow, cell.IndexColumn).RemoveGameElement(type);
    }

    public IEnumerable<DynamicGameObject> GetDynamicGameObjects(Int32 indexRow, Int32 indexColumn) {
        return GetCell(indexRow, indexColumn).DynamicGameObjects;
    }
    public CellWithGameObjects GetCell(Int32 indexRow, Int32 indexColumn) {
        return field[indexRow, indexColumn];
    }

    public IEnumerable<CellWithGameObjects> FindAll(GameObject element) {
        return FindAll(d => d.ToGameObject().Equals(element));
    }
    public IEnumerable<CellWithGameObjects> FindAll(DynamicGameObject element) {
        return FindAll(d => d.Equals(element));
    }
    public IEnumerable<CellWithGameObjects> FindAll(Type elementType) {
        return FindAll(d => d.IsDerived(elementType));
    }
    private IEnumerable<CellWithGameObjects> FindAll(Predicate<DynamicGameObject> predicate) {
        return this.Where(cell => cell.DynamicGameObjects.Exists(dynamicGameObject => predicate(dynamicGameObject)));
    }

    public IEnumerable<CellWithGameObjects> GetEmptyCells() {
        return this.Where(cell => cell.IsEmpty());
    }
    public Boolean OnField(Int32 indexRow, Int32 indexColumn) {
        return indexRow >= 0 && indexRow < Width && indexColumn >= 0 && indexColumn < Length;
    }

    public IEnumerator<CellWithGameObjects> GetEnumerator() {
        for(Int32 indexRow = 0; indexRow < Width; indexRow++)
            for(Int32 indexColumn = 0; indexColumn < Length; indexColumn++)
                yield return GetCell(indexRow, indexColumn);
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
