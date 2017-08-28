using UnityEngine;

public class EnemyAudioController : ObjectWithAudioSource {
    public AudioClip stepAudio;
    public AudioClip deathAfterBangAudio;
    public AudioClip tauntAudio;
    public AudioClip damageAudio;

    public void PlayStepAudio() {
        PlayOneShot(stepAudio, 0.3f);
    }
    public void PlayDeathAfterBangAudio() {
        PlayOneShot(deathAfterBangAudio);
    }
    public void PlayTauntAudio() {
        PlayOneShot(tauntAudio);
    }
    public void PlayDamageAudio() {
        PlayOneShot(damageAudio);
    }
}
