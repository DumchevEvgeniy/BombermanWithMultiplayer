using System;

public class WallpassPlayerSettings : MovementObjectSettings {
    protected override void OnStart() {
        base.OnStart();
        SetWallpassAbility(wallpass);
    }
    protected override void UpdateGameObjectStateHavingWallpass(Boolean wallpass) {
        base.UpdateGameObjectStateHavingWallpass(wallpass);
        SetWallpassAbility(wallpass);
    }
    protected override Boolean RoleIsCorrect() {
        return isLocalPlayer;
    }

    private void SetWallpassAbility(Boolean wallpass) {
        foreach(var cube in gameObject.scene.FindAllBreakCube())
            cube.GetComponent<BreakCubeSettings>().SetPlayerWallpass(wallpass, isLocalPlayer);
    }
}
