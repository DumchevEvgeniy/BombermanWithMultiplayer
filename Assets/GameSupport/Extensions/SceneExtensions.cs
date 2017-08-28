using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneExtensions {
    public const String mainCameraTag = "MainCamera";
    public const String levelTag = "Level";

    public static GameObject FindPlayer(this Scene scene) {
        return scene.FindPlayers().FirstOrDefault();
    }
    public static GameObject FindGameLevel(this Scene scene) {
        return scene.GetAllElementsByTag(levelTag).FirstOrDefault();
    }
    public static GameObject FindMainCamera(this Scene scene) {
        return scene.GetAllElementsByTag(mainCameraTag).FirstOrDefault();
    }

    public static Field GetField(this Scene scene) {
        var gameLevel = scene.FindGameLevel();
        if(gameLevel == null)
            return null;
        var level = gameLevel.GetComponent<Level>();
        return level == null ? null : level.Map.Field;
    }

    public static IEnumerable<GameObject> FindAllBreakCube(this Scene scene) {
        return GetAllElementsByTag(scene, BreakCube.tag);
    }
    public static IEnumerable<GameObject> FindPlayers(this Scene scene) {
        return GetAllElementsByTag(scene, Player.tag);
    }

    public static IEnumerable<GameObject> GetAllElementsByTag(this Scene scene, String tag) {
        return scene.GetRootGameObjects().Where(g => g.CompareTag(tag));
    }
}