using System;
using UnityEngine.Networking;

public class BombermanSettings : PlayerSettings {
    private const Int32 defaultCountBomb = 1;
    private const Int32 defaultBangDistance = 1;
    [SyncVar(hook = "OnUpdateCountBomb")]
    public Int32 maxCountBomb = defaultCountBomb;
    [SyncVar(hook = "OnUpdateBangDistance")]
    public Int32 bangDistance = defaultBangDistance;
    
    protected override void OnStart() {
        base.OnStart();
        InitializeBomberAbility();
    }

    protected override void UpdateData() {
        base.UpdateData();
        OnUpdateBangDistance(bangDistance);
        OnUpdateCountBomb(maxCountBomb);
    }

    private void InitializeBomberAbility() {
        if(GetComponent<BomberAbility>() == null)
            AddDefaultBomberAbility();
    }
    private void OnUpdateBangDistance(Int32 newBangDistance) {
        GetComponent<BomberAbility>().bangDistance = newBangDistance;
    }
    private void OnUpdateCountBomb(Int32 newCountBomb) {
        GetComponent<BomberAbility>().maxCountBomb = newCountBomb;
    }

    protected virtual void AddDefaultBomberAbility() {
        gameObject.AddComponent<BomberAbility>();
    }

    public void AddBomb() {
        maxCountBomb++;
    }
    public void AddBangDistance() {
        bangDistance++;
    }
    public void ResetBombCount() {
        maxCountBomb = defaultCountBomb;
    }
    public void ResetBangDistance() {
        bangDistance = defaultBangDistance;
    }
}