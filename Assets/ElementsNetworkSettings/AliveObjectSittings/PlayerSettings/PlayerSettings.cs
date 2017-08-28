using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSettings : WallpassPlayerSettings {
    [SyncVar] public Int32 numberOfLives = 3;
    [SyncVar] public Int32 gamePoints = 0;

    protected override void OnStart() {
        base.OnStart();
        gameObject.AddComponent<AudioListener>();
    }    
    public override void Die() {
        DeactivateCharacterController();
        DeactivateAudioListener();
        base.Die();
        numberOfLives--;
    }

    public void Initialize(Int32 numberOfLives, Int32 gamePoints) {
        this.numberOfLives = numberOfLives;
        this.gamePoints = gamePoints;
    }

    private void DeactivateAudioListener() {
        SetEnableForAudioListener(gameObject, false);
        var mainCamera = gameObject.scene.FindMainCamera();
        if(mainCamera != null)
            SetEnableForAudioListener(mainCamera, true);
    }
    private void SetEnableForAudioListener(GameObject gameObject, Boolean enable) {
        var audioListener = gameObject.GetComponent<AudioListener>();
        if(audioListener != null)
            audioListener.enabled = enable;
    }
    private void DeactivateCharacterController() {
        var character = GetComponent<CharacterController>();
        if(character != null)
            character.enabled = false;
    }

    [Command]
    public void CmdPlayTakeAnimation(GameObject player) {
        RpcPlayTakeAnimation(player);
    }

    [ClientRpc]
    private void RpcPlayTakeAnimation(GameObject player) {
        PlayerAnimator.PlayTake(player);
    }
}
