using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class DynamicGameObject {
    public const String tag = "Untagged";
    private List<Type> typesOfScriptsToAdd = new List<Type>();

    protected abstract String GetPrefabName();

    private GameObject CreateGameObject() {
        return GameObject.Instantiate<GameObject>(ToGameObject());
    }
    protected virtual void InitializeSettings(GameObject currentObject) { }

    public void AddScriptType(Type scriptType) {
        typesOfScriptsToAdd.Add(scriptType);
    }

    public GameObject ToGameObject() {
        return GameObjectLoader.Load(GetPrefabName());
    }
    public GameObject Create() {
        var gameObject = CreateGameObject();
        InitializeSettings(gameObject);
        foreach(var script in typesOfScriptsToAdd)
            gameObject.AddComponent(script);
        NetworkServer.Spawn(gameObject);
        return gameObject;
    }
   
    public Boolean IsDerived(System.Object obj) {
        if(obj == null)
            return false;
        return IsDerived(obj.GetType());
    }
    public Boolean IsDerived(Type objType) {
        if(objType == null)
            return false;
        var type = GetType();
        while(type != null) {
            if(objType == type)
                return true;
            type = type.BaseType;
        }
        return false;
    }

    public override Boolean Equals(System.Object obj) {
        if(obj == null)
            return false;
        return GetType() == obj.GetType();
    }
    public override Int32 GetHashCode() {
        return base.GetHashCode();
    }
}