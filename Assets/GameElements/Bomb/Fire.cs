using System;
using UnityEngine;

public class Fire : DynamicGameObject {
    private Cell position;
    private FireRotation rotation;
    private Int32 distance;
    private Single duration;

    public Fire(Cell position, FireRotation rotation, Int32 distance = 1, Single duration = 1.0f) {
        this.position = position;
        this.rotation = rotation;
        this.distance = distance;
        this.duration = duration;
    }

    protected override void InitializeSettings(GameObject fire) {
        fire.SetPosition(position.IndexRow, position.IndexColumn);
        fire.transform.rotation = Quaternion.Euler(0, (Int32)rotation, 0);
        var bangLineSetting = fire.GetComponent<BangLineSettings>();
        bangLineSetting.direction = GetDirection();
        bangLineSetting.distance = distance;
        bangLineSetting.duration = duration;
    }

    private Vector3 GetDirection() {
        if(rotation == FireRotation.Bottom)
            return new Vector3(1, 0, 0);
        if(rotation == FireRotation.Top)
            return new Vector3(-1, 0, 0);
        if(rotation == FireRotation.Right)
            return new Vector3(0, 0, 1);
        return new Vector3(0, 0, -1);
    }

    protected override String GetPrefabName() {
        return "Prefabs/Bomb/FireParticle";
    }

    public enum FireRotation : Int32 {
        Bottom = 0,
        Left = 90,
        Top = 180,
        Right = 270
    }
}

    
