using System;
using UnityEngine;
using UnityEngine.Networking;

class BreakCubeSettings : NetworkBehaviour {
    private void Start() {
        if(!isClient)
            return;
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1, 1, 1);
    }

    public void SetPlayerWallpass(Boolean playerWallpass, Boolean isLocalPlayer) {
        if(isLocalPlayer)
            GetComponent<BoxCollider>().isTrigger = playerWallpass;
    }
}