using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBangController {
    protected Action actionAfterBang = null;

    public virtual List<String> GetStoppedTags() {
        return new List<String>();
    }
    public virtual void ActionAfterBang() {
        if(actionAfterBang != null)
            actionAfterBang();
    }

    public abstract void ActionWithAttackedObjects(GameObject gameObject);
}