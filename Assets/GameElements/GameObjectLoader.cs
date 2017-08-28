using System;
using UnityEngine;

public static class GameObjectLoader {
    public static GameObject Load(String pathToResource) {
        return Resources.Load<GameObject>(pathToResource);
    }
}