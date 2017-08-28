using System;
using System.Collections;
using UnityEngine;

public abstract class SmoothMovementBase {
    public Single Speed { get; set; }
    public Single Distance { get; set; }
    public Vector3 Direction { get; set; }
    protected GameObject gameObject;
    private Boolean started = false;
    private Action<GameObject> postActions = null;

    public SmoothMovementBase(GameObject gameObject) {
        this.gameObject = gameObject;
    }

    protected abstract Int32 GetNormalCountFrame();
    protected abstract void FrameAction(GameObject gameObject);

    private void PostAction() {
        if(postActions == null)
            return;
        foreach(var postAction in postActions.GetInvocationList()) {
            var action = (Action<GameObject>)postAction;
            action(gameObject);
        }
    }
    public void AddPostAction(Action<GameObject> action) {
        postActions += action;
    }

    public IEnumerator MakeItSmooth() {
        started = true;
        Int32 counterFrame = 0;
        while(counterFrame < CountFrame) {
            FrameAction(gameObject);
            yield return new WaitForEndOfFrame();
            counterFrame++;
        }
        PostAction();
        started = false;
    }

    protected virtual Single Time {
        get { return Math.Abs(Distance) / Speed; }
    }
    protected virtual Int32 CountFrame {
        get { return (Int32)(Time * GetNormalCountFrame()); }
    }
    protected virtual Single MovingStep {
        get { return (Single)(1 / (Double)CountFrame); }
    }

    public Boolean Started {
        get { return started; }
    }
}