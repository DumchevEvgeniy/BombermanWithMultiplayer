using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BonusInAction : NetworkBehaviour {
    public BonusTypes bonusTypes;
    private Boolean started = false;

    private void OnTriggerEnter(Collider other) {
        if(!other.gameObject.CompareTag(Player.tag) || ExistBarrier())
            return;
        if(!started)
            StartCoroutine(PlayAnimatorAndTake(other.gameObject.GetParent()));
    }

    private IEnumerator PlayAnimatorAndTake(GameObject player) {
        started = true;
        if(PlayerAnimator.HasUser(player)) {
            player.GetComponent<PlayerSettings>().CmdPlayTakeAnimation(player);
            yield return new WaitForSeconds(0.3f);
        }
        TakeABonus(player);
        started = false;
    }

    private void TakeABonus(GameObject player) {
        switch(bonusTypes) {
            case BonusTypes.Bombs:
                ActionBonusBombs(player);
                break;
            case BonusTypes.Detonator:
                ActionBonusDetonator(player);
                break;
            case BonusTypes.Flames:
                ActionBonusFlames(player);
                break;
            case BonusTypes.Speed:
                ActionBonusSpeed(player);
                break;
            case BonusTypes.Wallpass:
                ActionBonusWallpass(player);
                break;
        }
        Destroy(gameObject);
    }

    private Boolean ExistBarrier() {
        var position = gameObject.GetIntegerPosition().Set(Coordinate.Y, 0);
        var hitObjects = new PlaneRay(position, new Vector3(0, 0, 0.45f), Vector3.forward) { Distance = 0.9f }.Cast();
        foreach(var hitElement in hitObjects) {
            var hitObject = hitElement.transform.gameObject.GetParent();
            if(!hitObject.OneFrom(Player.tag, Bonus.tag, Enemy.tag))
                return true;
        }
        return false;
    }

    private void ActionBonusBombs(GameObject gameObject) {
        var bombermanSettings = gameObject.GetComponent<BombermanSettings>();
        if(bombermanSettings == null)
            return;
        bombermanSettings.AddBomb();
    }
    private void ActionBonusDetonator(GameObject gameObject) {
        var cunningBombermanSettings = gameObject.GetComponent<CunningBombermanSettings>();
        if(cunningBombermanSettings == null)
            return;
        cunningBombermanSettings.preDetonatePossible = true;
    }
    private void ActionBonusFlames(GameObject gameObject) {
        var bombermanSettings = gameObject.GetComponent<BombermanSettings>();
        if(bombermanSettings == null)
            return;
        bombermanSettings.AddBangDistance();
    }
    private void ActionBonusSpeed(GameObject gameObject) {
        var movementSettings = gameObject.GetComponent<MovementObjectSettings>();
        if(movementSettings == null)
            return;
        movementSettings.movementSpeed++;
    }
    private void ActionBonusWallpass(GameObject gameObject) {
        var wallpassSettings = gameObject.GetComponent<WallpassPlayerSettings>();
        if(wallpassSettings == null)
            return;
        wallpassSettings.wallpass = true;
    }
}