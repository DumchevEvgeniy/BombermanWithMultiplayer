using System;
using UnityEngine.Networking;

public abstract class BaseBonusRotation : NetworkBehaviour {
    [SyncVar] public Single speed = 1.0f;

    private void Update() {
        Rotate();
    }

    public abstract void Rotate();
}
