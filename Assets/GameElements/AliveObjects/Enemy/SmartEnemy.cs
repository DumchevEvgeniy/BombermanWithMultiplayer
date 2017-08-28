using System;
using UnityEngine;

public class SmartEnemy : Enemy {
    private Field gameField;

    public SmartEnemy(String nickname, Int32 points, Field gameField) 
        : base(nickname, points) {
        this.gameField = gameField;
    }

    protected override void InitializeSettings(GameObject enemy) {
        base.InitializeSettings(enemy);
        enemy.GetComponent<EnemySettings>().gameField = gameField;
    }
}
