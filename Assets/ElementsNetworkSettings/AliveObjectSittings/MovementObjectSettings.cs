using System;
using UnityEngine.Networking;

public abstract class MovementObjectSettings : AliveObjectSettings {
    [SyncVar(hook = "OnUpdateMovementSpeed")]
    public Single movementSpeed = 1;
    [SyncVar(hook = "OnUpdateRotationSpeed")]
    public Single rotationSpeed = 2;
    [SyncVar(hook = "OnUpdateWallpass")]
    public Boolean wallpass = false;

    public void InitializeMovingSettings(Single movementSpeed, Single rotationSpeed, Boolean wallpass) {
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
        this.wallpass = wallpass;
    }

    protected virtual void UpdateGameObjectStateHavingWallpass(Boolean wallpass) {
        UpdateSkill(skill => skill.wallpass = wallpass);
    }

    protected override void OnUpdateAlive(Boolean isAlive) {
        UpdateSkill(skill => skill.alive = isAlive);
        base.OnUpdateAlive(isAlive);
    }
    protected override void UpdateData() {
        base.UpdateData();
        OnUpdateMovementSpeed(movementSpeed);
        OnUpdateRotationSpeed(rotationSpeed);
        OnUpdateWallpass(wallpass);
    }
    
    private void OnUpdateMovementSpeed(Single newMovementSpeed) {
        UpdateSkill(skill => skill.movementSpeed = newMovementSpeed);
    }
    private void OnUpdateRotationSpeed(Single newRotationSpeed) {
        UpdateSkill(skill => skill.rotationSpeed = newRotationSpeed);
    }
    private void OnUpdateWallpass(Boolean wallpass) {
        UpdateGameObjectStateHavingWallpass(wallpass);
    }
    private void UpdateSkill(Action<BaseMovementAbility> action) {
        var movementAbilities = GetComponents<BaseMovementAbility>();
        if(movementAbilities == null)
            return;
        foreach(var movementAbility in movementAbilities)
            action(movementAbility);
    }
}