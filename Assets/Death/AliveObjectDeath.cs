using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AliveObjectDeath : NetworkBehaviour {
    public void KillAliveObject(Func<IEnumerator> killFunc) {
        StartCoroutine(killFunc());
    }

    public IEnumerator PlayDieAfterBangAnimation(GameObject gameObject, Action postAction = null) {
        if(PlayerAnimator.HasUser(gameObject) || EnemyAnimator.HasUser(gameObject)) {
            CmdPlayDieAfterBangAnimation(gameObject);
            yield return new WaitForSeconds(6f);
        }
        if(postAction != null)
            postAction();
    }
    protected abstract Boolean TryPlayAnimation(GameObject gameObject);

    [Command]
    private void CmdPlayDieAfterBangAnimation(GameObject gameObject) {
        RpcPlayDieAfterBangAnimation(gameObject);
    }

    [ClientRpc]
    private void RpcPlayDieAfterBangAnimation(GameObject gameObject) {
        TryPlayAnimation(gameObject);
    }
}
