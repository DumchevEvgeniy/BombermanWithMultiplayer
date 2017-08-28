using UnityEngine;

public static class GameFactory {
    public static Enemy CreateEasyEnemy() {
        var prefabName = "Enemy";
        //var prefabName = "EnemyAgent";
        var enemy = new Enemy(prefabName, 100) {
            MovementSpeed = 1f,
            RotationSpeed = 50,
            Wallpass = false
        };
        enemy.AddScriptType(typeof(EnemyWithSmoothMovement));
        enemy.AddScriptType(typeof(EnemyDeath));
        return enemy;
    }
    public static Enemy CreateHardEnemy(Field gameField) {
        //var prefabName = "HardEnemyAgent";
        var prefabName = "HardEnemy";
        var enemy = new SmartEnemy(prefabName, 100, gameField) {
            MovementSpeed = 1.5f,
            RotationSpeed = 60,
            Wallpass = false
        };
        enemy.AddScriptType(typeof(EnemyWithSmartMovement));
        enemy.AddScriptType(typeof(EnemyDeath));
        return enemy;
    }

    public static Bonus CreateBonusBombs() {
        return new Bonus("Bombs", BonusTypes.Bombs, 600) {
            RotationSpeed = 5,
            RotationAngle = new Vector3(5, 10, 15)
        };
    }
    public static Bonus CreateBonusDetonator() {
        return new Bonus("Detonator", BonusTypes.Detonator, 10000) {
            RotationSpeed = 5,
            RotationAngle = new Vector3(10, 10, 3)
        };
    }
    public static Bonus CreateBonusFlames() {
        return new Bonus("Flames", BonusTypes.Flames, 400) {
            RotationSpeed = 6,
            RotationAngle = new Vector3(0, 0, Random.Range(5, 15))
        };
    }
    public static Bonus CreateBonusSpeed() {
        return new Bonus("Speed", BonusTypes.Speed, 1000) {
            RotationSpeed = 8,
            RotationAngle = new Vector3(0, Random.Range(5, 10), 0)
        };
    }
    public static Bonus CreateBonusWallpass() {
        return new Bonus("Wallpass", BonusTypes.Wallpass, 7000) {
            RotationSpeed = 7,
            RotationAngle = new Vector3(Random.Range(5, 15), Random.Range(5, 15), Random.Range(5, 15))
        };
    }
}
