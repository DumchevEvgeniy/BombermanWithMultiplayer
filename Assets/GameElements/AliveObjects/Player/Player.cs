using System;

public class Player : MovementObject {
    new public const String tag = "Player";
    public String PrefabName { get; set; }
    public Int32 StartGamePoints { get; set; }
    public Int32 StartNumberOfLives { get; set; }

    protected override String GetPrefabName() {
        return "Prefabs/Players/" + PrefabName;
    }
}
