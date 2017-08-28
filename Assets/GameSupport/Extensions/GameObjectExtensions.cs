using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class GameObjectExtensions {
    public static GameObject GetParent(this GameObject gameObject) {
        GameObject parentObject = gameObject;
        while(parentObject.transform.parent != null)
            parentObject = parentObject.transform.parent.gameObject;
        return parentObject;
    }

    public static Vector3 GetIntegerPosition(this GameObject gameObject) {
        var position = gameObject.transform.position;
        return new Vector3(position.x.ToRoundInt32(), position.y, position.z.ToRoundInt32());
    }

    public static Boolean OneFrom(this GameObject gameObject, params String[] tags) {
        if(tags == null || tags.Length == 0)
            return false;
        return tags.Any(tag => gameObject.CompareTag(tag));
    }

    public static void SetPosition(this GameObject gameObject, Coordinate coordinate, Single value) {
        gameObject.transform.position = gameObject.transform.position.Set(coordinate, value);
    }
    public static void SetPosition(this GameObject gameObject, Single x, Single z) {
        gameObject.transform.position = gameObject.transform.position.Set(x, z);
    }

    public static void SetScale(this GameObject gameObject, Single x, Single z) {
        gameObject.transform.localScale = new Vector3(x, gameObject.transform.localScale.y, z);
    }

    public static Boolean TryMakeActionWithAnimator(this GameObject gameObject, Action<Animator> action) {
        return gameObject.TryMakeActionWithComponent(action);
    }
    public static Boolean TryMakeActionWithNetworkAnimator(this GameObject gameObject, Action<NetworkAnimator> action) {
        return gameObject.TryMakeActionWithComponent(action);
    }
    public static Boolean TryMakeActionWithComponent<T>(this GameObject gameObject, Action<T> action) 
        where T : Component {
        var component = gameObject.GetComponent<T>();
        if(component == null)
            return false;
        action(component);
        return true;
    }
}