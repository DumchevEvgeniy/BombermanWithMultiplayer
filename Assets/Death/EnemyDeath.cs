using System;
using UnityEngine;

public class EnemyDeath : AliveObjectDeath {
    protected override Boolean TryPlayAnimation(GameObject gameObject) {
        return EnemyAnimator.PlayDeathAfterBang(gameObject);
    }
}