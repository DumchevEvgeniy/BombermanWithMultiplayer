using System;
using UnityEngine;

public class PlayerRotation : BaseMovementAbility {
    private const Int32 circleRight = 0;
    private const Int32 circleDown = 90;
    private const Int32 circleLeft = 180;
    private const Int32 circleUp = 270;
    private const Int32 circleDoubleRight = 360;
    private static System.Random random = new System.Random();

    protected override Boolean RoleIsCorrect() {
        return isLocalPlayer;
    }

    protected override void OnUpdate() {
        var newPosition = GetNewPosition();
        var oldPosition = transform.rotation.eulerAngles.y;
        var rotation = new Vector3(0, GetRotationCorner(oldPosition, newPosition), 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation, Space.World);
    }

    private Single GetNewPosition() {
        Single verticalOffset = GetOffsetByForceOfPressing(Input.GetAxis("Vertical"));
        Single horizontalOffset = GetOffsetByForceOfPressing(Input.GetAxis("Horizontal"));
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            return GetAverage(circleLeft + horizontalOffset, circleUp - verticalOffset);
        if(Input.GetKey(KeyCode.RightArrow ) && Input.GetKey(KeyCode.UpArrow))
            return GetAverage(circleDoubleRight - horizontalOffset, circleUp + verticalOffset);
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            return GetAverage(circleLeft - horizontalOffset, circleDown + verticalOffset);
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            return GetAverage(circleRight + horizontalOffset, circleDown - verticalOffset);
        if(Input.GetKey(KeyCode.LeftArrow))
            return circleLeft;
        if(Input.GetKey(KeyCode.RightArrow))
            return circleRight;
        if(Input.GetKey(KeyCode.DownArrow))
            return circleDown;
        if(Input.GetKey(KeyCode.UpArrow))
            return circleUp;
        return transform.rotation.eulerAngles.y;
    }
    private Single GetRotationCorner(Single oldPosition, Single newPosition) {
        var minPath = newPosition - oldPosition;
        if(Mathf.Abs(minPath) == circleLeft)
            return random.Next(0, 2) == 0 ? circleLeft : -circleLeft;
        if(minPath < -circleLeft)
            return minPath + circleDoubleRight;
        if(minPath > circleLeft)
            return minPath - circleDoubleRight;
        return minPath;
    }
    private Single GetOffsetByForceOfPressing(Single force) {
        return circleDown * (1 - Math.Abs(force));
    }
    private Single GetAverage(Single firstValue, Single secondValue) {
        return (Single)((firstValue + secondValue) / 2.0);
    }
}
