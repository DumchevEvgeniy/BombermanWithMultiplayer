using UnityEngine;

public class PlayerAudioController : ObjectWithAudioSource {
    public AudioClip stepAudio;
    public AudioClip deathAudio;
    public AudioClip deathAfterBangAudio;
    public AudioClip plantedBombAudio;
    public AudioClip takeABonusAudio;
    public AudioClip tauntAudio;

    public void PlayStepAudio() {
        PlayOneShot(stepAudio);
    }
    public void PlayDeathAudio() {
        PlayOneShot(deathAudio);
    }
    public void PlayPlantedBombAudio() {
        PlayOneShot(plantedBombAudio);
    }
    public void PlayTakeAbonusAudio() {
        PlayOneShot(takeABonusAudio);
    }
    public void PlayDeathAfterBangAudio() {
        PlayOneShot(deathAfterBangAudio);
    }
    public void PlayTauntAudio() {
        PlayOneShot(tauntAudio);
    }
}
