using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BangSettings : NetworkBehaviour {
    [SyncVar] public Int32 distance = 1;
    [SyncVar] public Single lifeTime = 1.0f;
    public List<String> stoppedTags = new List<String>();
    private List<GameObject> flames;
    private Action<GameObject> actionWithAttackedObjects = null;
    private Action actionAfterBang = null;
    
    private void Start() {
        if(!isServer)
            return;
        InitializeFlames();
        flames.ForEach(fireGameObject => {
            var fireSettings = fireGameObject.GetComponent<BangLineSettings>();
            fireSettings.distance = distance;
            fireSettings.duration = lifeTime;
            fireSettings.stoppedTags = stoppedTags;
            fireSettings.AddActionWithAttackedObjects(actionWithAttackedObjects);
        });
        StartCoroutine(StopBang());
    }

    public void AddActionAfterBang(Action action) {
        actionAfterBang += action;
    }
    public void AddActionWithAttackedObjects(Action<GameObject> action) {
        actionWithAttackedObjects += action;
    }

    private IEnumerator StopBang() {        
        yield return new WaitForSeconds(lifeTime);
        flames.ForEach(fireGameObjcet => Destroy(fireGameObjcet));
        Destroy(gameObject);
        StartActionAfterBang();
    }

    private void InitializeFlames() {
        var startPosition = gameObject.transform.position.ToCell();
        flames = new List<GameObject>();
        flames.Add(new Fire(startPosition, Fire.FireRotation.Bottom).Create());
        flames.Add(new Fire(startPosition, Fire.FireRotation.Top).Create());
        flames.Add(new Fire(startPosition, Fire.FireRotation.Left).Create());
        flames.Add(new Fire(startPosition, Fire.FireRotation.Right).Create());
    }
    private void StartActionAfterBang() {
        if(actionAfterBang == null)
            return;
        foreach(var action in actionAfterBang.GetInvocationList())
            ((Action)action)();
    }
}
