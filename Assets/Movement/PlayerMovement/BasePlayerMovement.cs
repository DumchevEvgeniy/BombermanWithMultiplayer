using System;
using UnityEngine;

public abstract class BasePlayerMovement : BaseMovementAbility {
    protected CharacterController characterController;

    protected override void OnStart() {
        base.OnStart();
        characterController = GetComponent<CharacterController>();
    }

    protected override Boolean RoleIsCorrect() {
        return isLocalPlayer;
    }
}