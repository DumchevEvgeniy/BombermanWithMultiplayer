using System;
using UnityEngine;

public class SmoothMovementForEnemy : SmoothMovementBase {
    private readonly Int32 normalCountFrame = 30;

    public SmoothMovementForEnemy(GameObject gameObject)
        : base(gameObject) {
        AddPostAction(PostAction);
    }

    protected override void FrameAction(GameObject gameObject) {
        Direction = gameObject.transform.forward;
        gameObject.transform.Translate(Direction * Distance * MovingStep, Space.World);
    }
    protected void PostAction(GameObject gameObject) {
        gameObject.transform.position = gameObject.GetIntegerPosition();
    }
    protected override Int32 GetNormalCountFrame() {
        return normalCountFrame;
    }
}
