using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerPlacement : NetworkBehaviour {
    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if(!isLocalPlayer)
            return;
        CmdSetPosition();
    }

    [Command]
    private void CmdSetPosition() {
        var level = gameObject.scene.FindGameLevel();
        if(level == null)
            return;
        var placementSeacher = level.GetComponent<NetworkPlayerPlacementSeacher>();
        if(placementSeacher == null)
            return;
        RpcSetStartPosition(placementSeacher.GetPlayerStartPosition());
    }

    [ClientRpc]
    private void RpcSetStartPosition(Vector3 startPosition) {
        gameObject.transform.position = startPosition;
    }
}
