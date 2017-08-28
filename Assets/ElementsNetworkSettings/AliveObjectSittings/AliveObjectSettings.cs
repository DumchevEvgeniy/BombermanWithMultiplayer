using System;
using UnityEngine.Networking;

public abstract class AliveObjectSettings : NetworkBehaviour {
    protected Boolean isAlive = true;

    private void Start() {
        if(!RoleIsCorrect())
            return;
        OnStart();
    }
    private void Update() {
        if(!RoleIsCorrect())
            return;
        OnUpdate();
    }

    protected virtual void OnStart() {
        UpdateData();
    }
    protected virtual void OnUpdate() {
        return;
    }
    protected virtual void OnUpdateAlive(Boolean isAlive) { }
    protected virtual void UpdateData() {
        OnUpdateAlive(isAlive);
    }

    protected abstract Boolean RoleIsCorrect();
    
    public virtual void Die() {
        isAlive = false;
        OnUpdateAlive(isAlive);
    }
    public Boolean IsAlive() {
        return isAlive;
    }
}
