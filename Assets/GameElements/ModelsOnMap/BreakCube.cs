using System;
using UnityEngine;

public class BreakCube : DynamicGameObject {
    new public const String tag = "BreakCube";

    protected override String GetPrefabName() {
        return "Prefabs/Design/BreakCube";
    }
}
