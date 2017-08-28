using System;
using UnityEngine;

public static class EnemyAnimator {
    public const String controllerName = "EnemyAnimatorController";
    public const String parameterDeathAfterBang = "DeathAfterBang";
    public const String parameterKillPlayer = "KillPlayer";
    public const String parameterCanRun = "CanRun";

    public static Boolean HasUser(GameObject gameObject) {
        var animator = gameObject.GetComponent<Animator>();
        if(animator == null)
            return false;
        return animator.runtimeAnimatorController.name == controllerName;
    }

    public static Boolean PlayDeathAfterBang(GameObject gameObject) {
        return gameObject.TryMakeActionWithNetworkAnimator(a => a.SetTrigger(parameterDeathAfterBang));
    }
    public static Boolean PlayKillPlayer(GameObject gameObject) {
        return gameObject.TryMakeActionWithNetworkAnimator(a => a.SetTrigger(parameterKillPlayer));
    }
    public static Boolean PlayRun(GameObject gameObject) {
        return gameObject.TryMakeActionWithNetworkAnimator(a => a.SetTrigger(parameterCanRun));
    }
}
