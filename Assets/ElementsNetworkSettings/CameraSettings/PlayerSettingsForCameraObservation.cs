using UnityEngine.Networking;

public class PlayerSettingsForCameraObservation : NetworkBehaviour {
    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        SetCameraAboveThePlayer();
    }

    private void SetCameraAboveThePlayer() {
        var mainCamera = gameObject.scene.FindMainCamera();
        mainCamera.GetComponent<CameraSettings>().SetObjectOfObservation(gameObject);
    }
}
