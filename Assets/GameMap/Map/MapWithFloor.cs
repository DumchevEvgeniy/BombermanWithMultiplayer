using System;
using UnityEngine;

public class MapWithFloor : BaseMap {
    public MapWithFloor(Int32 length, Int32 width, DynamicGameObject gameFloorTemplate) 
        : base(length, width) {
        InitializeMap(gameFloorTemplate);
    }

    private void InitializeMap(DynamicGameObject gameFloorTemplate) {
        GameObject gameFloor = gameFloorTemplate.Create();
        Single offsetByX = (Int32)(Width / 2.0);
        Single offsetByZ = (Int32)(Length / 2.0);
        gameFloor.SetPosition(offsetByX, offsetByZ);
        var floorNetworkSettings = gameFloor.GetComponent<FloorNetworkSettings>();
        floorNetworkSettings.SetScale(Width, Length);
        floorNetworkSettings.UpdateScale();
    }
}
