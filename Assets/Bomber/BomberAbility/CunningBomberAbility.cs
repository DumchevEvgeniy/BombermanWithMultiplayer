using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CunningBomberAbility : BomberAbility {
    public Boolean enable = true;
    private List<GameObject> mortgagedBombs = new List<GameObject>();

    protected override void OnUpdate() {
        base.OnUpdate();
        if(!enable)
            return;
        if(!Input.GetKeyDown(KeyCode.LeftShift))
            return;
        if(mortgagedBombs.IsEmpty())
            return;
        if(PlayerAnimator.HasUser(gameObject))
            CmdPlayDetonateAnimation(gameObject);
        CmdDetonateABomb(GetFirstBomb());
    }
    
    protected override void OnDetonateBomb() {
        if(!mortgagedBombs.IsEmpty())
            mortgagedBombs.RemoveAt(0);
        base.OnDetonateBomb();
    }
    protected override void OnPlantBomb(GameObject bomb) {
        mortgagedBombs.Add(bomb);
        base.OnPlantBomb(bomb);
    }

    [Command]
    private void CmdDetonateABomb(GameObject bomb) {
        bomb.GetComponent<BombSettings>().DetonateABomb();
    }
    [Command]
    private void CmdPlayDetonateAnimation(GameObject gameObject) {
        RpcPlayDetonateAnimation(gameObject);
    }

    [ClientRpc]
    private void RpcPlayDetonateAnimation(GameObject gameObject) {
        PlayerAnimator.PlayDetonate(gameObject);
    }

    private GameObject GetFirstBomb() {
        return mortgagedBombs.FirstOrDefault();
    }
}
