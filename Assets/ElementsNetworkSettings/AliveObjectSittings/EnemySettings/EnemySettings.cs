using System;
using UnityEngine;

public class EnemySettings : MovementObjectSettings {
    public String nickname;
    public Int32 points;
    public Field gameField;

    public void Initialize(String nickname, Int32 points) {
        this.nickname = nickname;
        this.points = points;
    }

    public override void Die() {
        foreach(var collider in GetComponentsInChildren<Collider>())
            collider.isTrigger = true;
        base.Die();
    }

    protected override Boolean RoleIsCorrect() {
        return isServer;
    }
}
