using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : BasePlayerMovement {
    protected override void OnUpdate() {
        if(characterController == null || !characterController.enabled)
            return;
        Single deltaX = -Input.GetAxis("Vertical") * movementSpeed;
        Single deltaZ = Input.GetAxis("Horizontal") * movementSpeed;
        var movement = new Vector3(deltaX, 0, deltaZ);
        characterController.SimpleMove(movement);
        if(PlayerAnimator.HasUser(gameObject))
            CmdPlayRunAnimation(gameObject, deltaX != 0 || deltaZ != 0);        
    }

    [Command]
    private void CmdPlayRunAnimation(GameObject gameObject, Boolean value) {
        RpcPlayRunAnimation(gameObject, value);
    }

    [ClientRpc]
    private void RpcPlayRunAnimation(GameObject gameObject, Boolean value) {
        PlayerAnimator.PlayRun(gameObject, value);
    }
}