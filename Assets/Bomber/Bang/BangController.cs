using System;
using System.Collections.Generic;
using UnityEngine;

public class BangController : BaseBangController {
    public override List<String> GetStoppedTags() {
        return new List<String>() {
            ConcreteCube.tag,
            Bonus.tag,
            BreakCube.tag
        };
    }

    public override void ActionWithAttackedObjects(GameObject gameObject) {
        if(gameObject.OneFrom(Player.tag, Enemy.tag))
            KillAliveObject(gameObject);
        if(gameObject.CompareTag(BreakCube.tag))
            KillBreakCube(gameObject);
        if(gameObject.CompareTag(Bonus.tag))
            KillBonus(gameObject);
    }

    private void KillAliveObject(GameObject gameObject) {
        var settings = gameObject.GetComponent<AliveObjectSettings>();
        if(!settings.IsAlive())
            return;
        settings.Die();
        var aliveObjectDeath = gameObject.GetComponent<AliveObjectDeath>();
        if(aliveObjectDeath == null) {
            GameObject.Destroy(gameObject);
            return;
        }
        aliveObjectDeath.KillAliveObject(() => 
            aliveObjectDeath.PlayDieAfterBangAnimation(gameObject,() => GameObject.Destroy(gameObject)));
    }
    private void KillBreakCube(GameObject gameObject) {
        var field = gameObject.scene.GetField();
        if(field != null) {
            var position = gameObject.GetIntegerPosition();
            field.RemoveElement((Int32)position.x, (Int32)position.z, typeof(BreakCube));
        }
        GameObject.Destroy(gameObject);
    }
    private void KillBonus(GameObject gameObject) {
        var savePosition = gameObject.transform.position;
        GameObject.Destroy(gameObject);
        actionAfterBang += (() => KillBonusPenalty(savePosition));
    }
    private void KillBonusPenalty(Vector3 position) {
        Int32 enemyPenaltyCount = 10;
        for(Int32 i = 0; i < enemyPenaltyCount; i++)
            GameFactory.CreateEasyEnemy().Create()
                .SetPosition(position.x.ToRoundInt32(), position.z.ToRoundInt32());
    }
}