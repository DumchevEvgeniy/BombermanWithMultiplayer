using System;
using UnityEngine;

public class SmoothRotation : SmoothMovementBase {
    private readonly Int32 normalCountFrame = 10; 

    public SmoothRotation(GameObject gameObject)
        : base(gameObject) {
        AddPostAction(PostAction);
    }

    protected override void FrameAction(GameObject gameObject) {
        gameObject.transform.Rotate(Direction * Distance * MovingStep, Space.Self);
    }
    protected void PostAction(GameObject gameObject) {
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.ToRoundInt32());
    }
    protected override Int32 GetNormalCountFrame() {
        return normalCountFrame;
    }
}
