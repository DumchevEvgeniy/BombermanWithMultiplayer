using System;
using UnityEngine;

public class SmoothMovementForPlayer : SmoothMovementBase {
    private readonly Int32 normalCountFrame = 30;

    private CharacterController characterController;

    public SmoothMovementForPlayer(GameObject gameObject) : base(gameObject) {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    protected override void FrameAction(GameObject gameObject) {
        characterController.Move(Direction * Distance * MovingStep);
    }
    protected override Int32 GetNormalCountFrame() {
        return normalCountFrame;
    }
}