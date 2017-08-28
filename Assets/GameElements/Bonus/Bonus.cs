using System;
using UnityEngine;

public class Bonus : DynamicGameObject {
    new public const String tag = "Bonus";
    private readonly String name;
    private readonly Int32 points;
    private readonly BonusTypes bonusType;
    private Vector3 rotationAngle = default(Vector3);
    private Single rotationSpeed = 1;
    
    public Bonus(String name, BonusTypes bonusType, Int32 points) {
        this.name = name;
        this.points = points;
        this.bonusType = bonusType;
    }

    protected override void InitializeSettings(GameObject bonus) {
        var bonusInAction = bonus.AddComponent<BonusInAction>();
        bonusInAction.bonusTypes = bonusType;
        var rotation = bonus.AddComponent<CircleBonusRotation>();
        rotation.stepAngle = RotationAngle;
        rotation.speed = RotationSpeed;
    }

    protected override String GetPrefabName() {
        return "Prefabs/Bonuses/" + Name; 
    }

    public String Name { get { return name; } }
    public Int32 Points { get { return points; } }
    public Vector3 RotationAngle {
        get { return rotationAngle; }
        set { rotationAngle = value; }
    }
    public Single RotationSpeed {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }
}
