using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class BangLineSettings : NetworkBehaviour {
    [SyncVar] public Vector3 direction;
    [SyncVar] public Int32 distance = 1;
    [SyncVar] public Single duration = 1.0f;
    public List<String> stoppedTags = new List<String>();
    private Action<GameObject> actionWithAttackedObjects = null;
    private PlaneRay bangRay;
    private List<GameObject> hitObjects = new List<GameObject>();
    
    private void Start() {
        if(!isServer)
            return;
        bangRay = new PlaneRay(gameObject.transform.position, direction) { Distance = RealDistance };
        InitializeDistance();
        RpcBangSimulate(gameObject, distance, duration);
    }
    private void Update() {
        if(!isServer)
            return;
        var currentHitObjects = bangRay.Cast();
        foreach(var hitObject in currentHitObjects) {
            var gameElement = hitObject.transform.gameObject.GetParent();
            if(hitObjects.Contains(gameElement))
                continue;
            Callback(gameElement);
        }
    }

    [ClientRpc]
    private void RpcBangSimulate(GameObject bangObject, Int32 distance, Single duration) {
        var bang = GetComponent<ParticleSystem>();
        var main = bang.main;
        main.duration = duration;
        main.startSpeed = distance + 0.2f;
        main.simulationSpeed = distance * 40;
        bang.Simulate(duration);
    }

    public void AddActionWithAttackedObjects(Action<GameObject> action) {
        actionWithAttackedObjects += action;
    }

    private void Callback(GameObject gameObject) {
        hitObjects.Add(gameObject);
        if(actionWithAttackedObjects == null)
            return;
        foreach(var action in actionWithAttackedObjects.GetInvocationList())
            ((Action<GameObject>)action)(gameObject);
    }
    private void InitializeDistance() {
        var hitGameElements = bangRay.Cast();
        if(!ExistStoppedElement(hitGameElements))
            return;
        var firstStoppedElement = GetFirstStoppedElement(hitGameElements).transform.gameObject.GetParent();
        var distanceBetweenTwoObjects = firstStoppedElement.transform.position - gameObject.transform.position;
        distance = GetNotZeroCoordinate(distanceBetweenTwoObjects);
        bangRay.Distance = RealDistance - 1;
        Callback(firstStoppedElement);
    }

    private RaycastHit GetFirstStoppedElement(IEnumerable<RaycastHit> hitElements) {
        return hitElements.FirstOrDefault(ContainsInStoppedTags);
    }
    private Boolean ExistStoppedElement(IEnumerable<RaycastHit> hitElements) {
        return hitElements.ToList().Exists(ContainsInStoppedTags);
    }
    private Boolean ContainsInStoppedTags(RaycastHit hit) {
        return hit.transform.gameObject.OneFrom(stoppedTags.ToArray());
    }

    private Int32 GetNotZeroCoordinate(Vector3 distance) {
        if(distance.x != 0)
            return Math.Abs(distance.x.ToRoundInt32());
        if(distance.y != 0)
            return Math.Abs(distance.y.ToRoundInt32());
        return Math.Abs(distance.z.ToRoundInt32());
    }

    private Single RealDistance {
        get { return distance + 0.45f; }
    }
}
