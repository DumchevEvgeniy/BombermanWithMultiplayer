using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BombSettings : NetworkBehaviour {
    /*[SyncVar] */public Int32 distance = 1;
    /*[SyncVar] */public Single timeOfDeath = 2.0f;
    /*[SyncVar] */public Single bangLifeTime = 2.0f;
    private Action actionAfterDeath = null;
    private BoxCollider boxCollider;
    public BaseBangController bangController;

    public void Start() {
        if(!isServer)
            return;
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(1, 1, 1);
        boxCollider.isTrigger = true;
        StartCoroutine(Die());
    }
    public void Update() {
        if(!isServer)
            return;
        if(!boxCollider.isTrigger)
            return;
        boxCollider.isTrigger = !CanResetTrigger();
    }

    public void DetonateABomb() {
        RemoveFromMap(gameObject);
        Destroy(gameObject);
        StopCoroutine(Die());
        MakeABang();
        PreformActionsAfterBang();
    }
    public void AddActionAfterDeath(Action action) {
        actionAfterDeath += action;
    }

    private void MakeABang() {
        var bang = new Bang(transform.position.ToCell()).Create();
        var bangSettings = bang.GetComponent<BangSettings>();
        bangSettings.distance = distance;
        bangSettings.lifeTime = bangLifeTime;
        if(bangController == null)
            return;
        bangSettings.stoppedTags = bangController.GetStoppedTags();
        bangSettings.AddActionWithAttackedObjects(bangController.ActionWithAttackedObjects);
        bangSettings.AddActionAfterBang(bangController.ActionAfterBang);
    }
    private void PreformActionsAfterBang() {
        if(actionAfterDeath == null)
            return;
        foreach(var action in actionAfterDeath.GetInvocationList())
            ((Action)action)();
    }
    private void RemoveFromMap(GameObject gameObject) {
        var field = gameObject.scene.GetField();
        if(field != null)
            field.RemoveElement(gameObject.transform.position.ToCell(), typeof(Bomb));
    }

    private IEnumerator Die() {
        yield return new WaitForSeconds(timeOfDeath);
        DetonateABomb();
    }

    private Boolean CanResetTrigger() {
        var size = boxCollider.transform.localScale.z;
        var halfSize = size / 2;
        var centerPosition = gameObject.GetIntegerPosition();
        var planeRay = new PlaneRay(centerPosition, new Vector3(0, 0, halfSize), Vector3.forward) {
            Distance = size,
            HalfLengthBox = halfSize
        };
        foreach(var hitElement in planeRay.Cast()) {
            var hitObject = hitElement.transform.gameObject.GetParent();
            if(hitObject.CompareTag(Player.tag))
                return false;
        }
        return true;
    }
}