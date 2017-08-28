using System;
using UnityEngine.Networking;

public class FloorNetworkSettings : NetworkBehaviour {
    [SyncVar] private Single width = 10;
    [SyncVar] private Single length = 10;

    public void SetScale(Single width, Single length) {
        this.width = width;
        this.length = length;
    }

    public override void OnStartClient() {
        UpdateScale();
    }

    public void UpdateScale() {
        gameObject.SetScale(width, length);
    }
}
