using System;
using UnityEngine;

public abstract class EnemyWithSmoothMovementBase : BaseMovementAbility {
    public Single movementDistance = 1f;
    protected SmoothMovementForEnemy smoothMovement;
    protected SmoothRotation smoothRotation;
    public Boolean enable = true;

    protected override Boolean RoleIsCorrect() {
        return isServer;
    }

    protected override void OnStart() {
        base.OnStart();
        smoothRotation = new SmoothRotation(gameObject) {
            Distance = GetRotationAngle(),
            Speed = rotationSpeed,
            Direction = Vector3.up
        };
        smoothMovement = new SmoothMovementForEnemy(gameObject) {
            Distance = movementDistance,
            Speed = movementSpeed,
        };
        smoothMovement.AddPostAction(RotateAfterMoving);
    }
    protected override void OnUpdate() {
        base.OnUpdate();
        if(smoothMovement.Started || smoothRotation.Started)
            return;
        if(!enable)
            return;
        if(CanMove()) {
            smoothMovement.Speed = movementSpeed;
            StartCoroutine(smoothMovement.MakeItSmooth());
        }
        else
            StartSmoothRotating();
    }

    private void StartSmoothRotating() {
        smoothRotation.Distance = GetRotationAngle();
        smoothRotation.Speed = rotationSpeed;
        StartCoroutine(smoothRotation.MakeItSmooth());
    }
    private void RotateAfterMoving(GameObject gameObject) {
        if(NeededRotateAfterMoving(gameObject))
            StartSmoothRotating();
    }

    protected abstract Boolean CanMove();
    protected abstract Boolean NeededRotateAfterMoving(GameObject gameObject);
    protected abstract Single GetRotationAngle();
}
