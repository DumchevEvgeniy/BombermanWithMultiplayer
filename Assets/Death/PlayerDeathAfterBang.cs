using System;
using UnityEngine;

public class PlayerDeathAfterBang : AliveObjectDeath {
    protected override Boolean TryPlayAnimation(GameObject gameObject) {
        return PlayerAnimator.PlayDeathAfterBang(gameObject);
    }
}
