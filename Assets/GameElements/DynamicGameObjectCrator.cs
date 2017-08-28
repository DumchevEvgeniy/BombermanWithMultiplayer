using System;

public class DynamicGameObjectCreator : DynamicGameObject {
    String prefabName;

    public DynamicGameObjectCreator(String prefabName) {
        this.prefabName = prefabName;
    }

    protected override String GetPrefabName() {
        return prefabName;
    }
}
