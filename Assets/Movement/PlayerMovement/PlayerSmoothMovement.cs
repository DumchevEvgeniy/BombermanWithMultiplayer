using System;
using UnityEngine;

public class PlayerSmoothMovement : BasePlayerMovement {
    private SmoothMovementForPlayer smoothMovement;
    public Single movementDistance = 1f;

    protected override void OnStart() {
        base.OnStart();
        smoothMovement = new SmoothMovementForPlayer(gameObject);
    }

    protected override void OnUpdate() {
        base.OnUpdate();
        if(smoothMovement.Started)
            return;
        var deltaX = Input.GetAxis("Vertical");
        var deltaZ = Input.GetAxis("Horizontal");
        if(deltaX == 0 && deltaZ == 0)
            return;
        Single xDerection = -Math.Sign(deltaX);
        Single zDerection = Math.Sign(deltaZ);
        smoothMovement.Direction = new Vector3(xDerection, 0, zDerection);
        smoothMovement.Distance = movementDistance * GetHypotenuse(deltaX, deltaZ);
        smoothMovement.Speed = movementSpeed;
        StartCoroutine(smoothMovement.MakeItSmooth());
    }

    private Single GetHypotenuse(Single firstCathenus, Single secondCathenus) {
        return (Single)Math.Sqrt(Math.Pow(firstCathenus, 2) + Math.Pow(secondCathenus, 2));
    }
}
