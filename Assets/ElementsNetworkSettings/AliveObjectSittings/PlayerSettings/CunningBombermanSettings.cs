using System;
using UnityEngine.Networking;

public class CunningBombermanSettings : BombermanSettings {
    [SyncVar(hook = "OnUpdatePreDetonatePossible")]
    public Boolean preDetonatePossible = false;

    protected override void AddDefaultBomberAbility() {
        gameObject.AddComponent<CunningBomberAbility>();
        OnUpdatePreDetonatePossible(preDetonatePossible);
    }

    protected override void UpdateData() {
        base.UpdateData();
        OnUpdatePreDetonatePossible(preDetonatePossible);
    }

    private void OnUpdatePreDetonatePossible(Boolean newPreDetonatePossible) {
        gameObject.GetComponent<CunningBomberAbility>().enable = newPreDetonatePossible;
    }
}