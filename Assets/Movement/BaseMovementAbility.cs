using System;
using UnityEngine.Networking;

public abstract class BaseMovementAbility : NetworkBehaviour {
    [SyncVar] public Boolean alive = true;
    public Single movementSpeed = 1.0f;
    public Single rotationSpeed = 1.0f;
    public Boolean wallpass = false;

    void Start() {
        if(!RoleIsCorrect())
            return;
        OnStart();
    }
    void Update() {
        if(!RoleIsCorrect() || !alive)
            return;
        OnUpdate();
    }

    protected abstract Boolean RoleIsCorrect();

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
}