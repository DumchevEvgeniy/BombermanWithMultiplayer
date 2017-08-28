using System;
using UnityEngine;

public class Enemy : MovementObject {
    new public const String tag = "Enemy";
    public String Nickname { get; private set; }
    public Int32 Points { get; private set; }
    
    public Enemy(String nickname, Int32 points) {
        Nickname = nickname;
        Points = points;
    }

    protected override void InitializeSettings(GameObject enemy) {
        var enemySettings = enemy.AddComponent<EnemySettings>();
        enemySettings.InitializeMovingSettings(MovementSpeed, RotationSpeed, Wallpass);
        enemySettings.Initialize(Nickname, Points);
    }

    protected override String GetPrefabName() {
        return "Prefabs/Enemies/" + Nickname;
    }
}
