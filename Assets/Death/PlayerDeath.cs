using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerDeath : PlayerDeathAfterBang {
    public void OnTriggerEnter(Collider other) {
        if(!isLocalPlayer)
            return;
        var playerSettings = GetComponent<PlayerSettings>();
        if(playerSettings == null || !playerSettings.IsAlive())
            return;
        var otherObject = other.gameObject.GetParent();
        if(!otherObject.CompareTag(Enemy.tag))
            return;
        playerSettings.Die();
        CmdKillPlayer(otherObject);
    }

    [Command]
    private void CmdKillPlayer(GameObject enemy) {
        if(EnemyHasAnimation(enemy)) {
            StartCoroutine(PlayEnemyAnimation(enemy));
            TargetKillPlayer(connectionToClient, enemy, false);
        }
        else
            TargetKillPlayer(connectionToClient, enemy, true);
    }
    [Command]
    private void CmdDestroy(GameObject gameObject) {
        Destroy(gameObject);
    }
    [Command]
    private void CmdPlayPlayerDeathAnimation(GameObject gameObject) {
        RpcPlayPlayerDeathAnimation(gameObject);
    }

    [TargetRpc]
    private void TargetKillPlayer(NetworkConnection networkConnection, GameObject enemy, Boolean needToDestroy) {
        if(PlayerAnimator.HasUser(gameObject))
            StartCoroutine(PlayPlayerAnimation(gameObject, needToDestroy));
        else
            DieByDefault(-enemy.transform.forward, needToDestroy);
    }
    [ClientRpc]
    private void RpcPlayPlayerDeathAnimation(GameObject gameObject) {
        PlayerAnimator.PlayDeath(gameObject);
    }

    private void DieByDefault(Vector3 direction, Boolean needToDestroy) {
        var smoothRotation = new SmoothRotation(gameObject) {
            Distance = -90,
            Direction = direction,
            Speed = 30f
        };
        if(needToDestroy)
            smoothRotation.AddPostAction(g => CmdDestroy(gameObject));
        StartCoroutine(smoothRotation.MakeItSmooth());
    }

    private IEnumerator PlayEnemyAnimation(GameObject enemy) {
        var enemyMovement = enemy.GetComponent<EnemyWithSmoothMovementBase>();
        enemyMovement.enable = false;
        yield return new WaitForSeconds(6f);
        EnemyAnimator.PlayRun(enemy);
        enemyMovement.enable = true;
        Destroy(gameObject);
    }
    private IEnumerator PlayPlayerAnimation(GameObject player, Boolean needToDestroy) {
        CmdPlayPlayerDeathAnimation(player);
        if(needToDestroy) {
            yield return new WaitForSeconds(6.0f);
            CmdDestroy(player);
        }
    }

    private Boolean EnemyHasAnimation(GameObject enemy) {
        return EnemyAnimator.PlayKillPlayer(enemy);
    }
}
