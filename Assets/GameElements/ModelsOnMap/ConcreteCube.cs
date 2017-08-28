using System;

public class ConcreteCube : DynamicGameObject {
    new public const String tag = "ConcreteCube";
    
    protected override String GetPrefabName() {
        return "Prefabs/Design/ConcreteCube";
    }
}