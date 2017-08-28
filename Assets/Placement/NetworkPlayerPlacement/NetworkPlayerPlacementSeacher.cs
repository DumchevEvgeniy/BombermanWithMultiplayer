using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerPlacementSeacher : NetworkBehaviour {
    private static System.Random random = new System.Random();

    public Vector3 GetPlayerStartPosition() {
        var field = GetComponent<Level>().Map.Field;
        var possibleCells = new List<Cell>();
        foreach(var cell in field.GetEmptyCells())
            if(ExistEmptyPlace(cell))
                possibleCells.Add(cell);
        if(possibleCells.IsEmpty())
            return default(Vector3);
        var startPosition =  possibleCells[random.Next(0, possibleCells.Count)];
        return new Vector3(startPosition.IndexRow, 0, startPosition.IndexColumn);
    }

    private Boolean ExistEmptyPlace(Cell cell) {
        var centerPosition = new Vector3(cell.IndexRow, 0, cell.IndexColumn);
        Int32 counterCellsForMovement = 0;
        if(!ExistEmptyPlace(centerPosition, Vector3.forward, ref counterCellsForMovement))
            return false;
        if(!ExistEmptyPlace(centerPosition, Vector3.back, ref counterCellsForMovement))
            return false;
        if(!ExistEmptyPlace(centerPosition, Vector3.right, ref counterCellsForMovement))
            return false;
        if(!ExistEmptyPlace(centerPosition, Vector3.left, ref counterCellsForMovement))
            return false;
        return counterCellsForMovement >= 2;
    }
    private Boolean ExistEmptyPlace(Vector3 centerPosition, Vector3 direction, ref Int32 counterCellsForMovement) {
        var statusResult = CastRay(centerPosition, direction);
        if(statusResult == HitObjectStatus.existAliveBarrier)
            return false;
        counterCellsForMovement += (Int32)statusResult;
        return true;
    }
    private HitObjectStatus CastRay(Vector3 centerPosition, Vector3 direction) {
        Vector3 offsetPosition = direction * 0.49f;
        var hitObjects = new PlaneRay(centerPosition, offsetPosition, direction) { Distance = 1.98f }.Cast();
        foreach(var hit in hitObjects) {
            var element = hit.transform.gameObject.GetParent();
            if(element.OneFrom(Player.tag, Enemy.tag))
                return HitObjectStatus.existAliveBarrier;
            if(element.OneFrom(ConcreteCube.tag, BreakCube.tag))
                return HitObjectStatus.besideWall;
        }
        return HitObjectStatus.allClear;
    }

    private enum HitObjectStatus : Int32 {
        allClear = 1,
        besideWall = 0,
        existAliveBarrier = -1,
    }
}
