using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BomberAbility : NetworkBehaviour {
    public Int32 bangDistance = 1;
    public Int32 maxCountBomb = 1;
    protected Int32 bombCounter = 0;
    private Boolean bombIsBeingPlanted = false;

    private void Update() {
        if(isLocalPlayer)
            OnUpdate();
    }

    protected virtual void OnUpdate() {
        if(!Input.GetKeyDown(KeyCode.Space) || bombIsBeingPlanted)
            return;
        if(!BombsAreAvailable() || ExistBarrier())
            return;
        var position = gameObject.GetIntegerPosition();
        StartCoroutine(PlantBombWithAnimation(position));
    }
    protected virtual void OnPlantBomb(GameObject gameObject) {
        bombCounter++;
    }
    protected virtual void OnDetonateBomb() {
        bombCounter--;
    }

    [Command]
    private void CmdCreateBomb(Vector3 position) {
        var cellForBomb = position.ToCell();
        var bomb = new Bomb(cellForBomb, bangDistance, 2, 1) {
            ActionAfterDeath = () => TargetOnDetonateBomb(connectionToClient),
            BangController = new BangController()
        };
        var field = gameObject.scene.GetField();
        if(field != null)
            field.AddElement(cellForBomb, bomb);
        RpcOnPlantBomb(bomb.Create());
    }
    [Command]
    private void CmdPlayPlantedBombAnimation(GameObject gameObject) {
        RpcPlayPlantedBombAnimation(gameObject);
    }
    
    [TargetRpc]
    private void TargetOnDetonateBomb(NetworkConnection target) {
        OnDetonateBomb();
    }
    [ClientRpc]
    private void RpcOnPlantBomb(GameObject gameObject) {
        if(!isLocalPlayer)
            return;
        OnPlantBomb(gameObject);
    }
    [ClientRpc]
    private void RpcPlayPlantedBombAnimation(GameObject gameObject) {
        PlayerAnimator.PlayPlantedBomb(gameObject);
    }

    private IEnumerator PlantBombWithAnimation(Vector3 position) {
        bombIsBeingPlanted = true;
        if(PlayerAnimator.HasUser(gameObject)) {
            CmdPlayPlantedBombAnimation(gameObject);
            yield return new WaitForSeconds(0.3f);
        }
        CmdCreateBomb(position);
        bombIsBeingPlanted = false;
    }

    private Boolean BombsAreAvailable() {
        return maxCountBomb > 0 && bombCounter < maxCountBomb;
    }
    private Boolean ExistBarrier() {
        var position = gameObject.GetIntegerPosition().Set(Coordinate.Y, 0);
        var hitObjects = new PlaneRay(position, new Vector3(0, 0, 0.45f), Vector3.forward) { Distance = 0.9f }.Cast();
        foreach(var hitElement in hitObjects) {
            var hitObject = hitElement.transform.gameObject.GetParent();
            if(hitObject.OneFrom(BreakCube.tag, Bonus.tag, Enemy.tag, Bomb.tag))
                return true;
        }
        return false;
    }
    private Boolean IsBreakCubeOrBomb(Collider other) {
        return other.gameObject.GetParent().OneFrom(BreakCube.tag, Bomb.tag);
    }
}
