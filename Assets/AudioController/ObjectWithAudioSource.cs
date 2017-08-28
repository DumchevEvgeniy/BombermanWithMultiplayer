using System;
using UnityEngine;
using UnityEngine.Internal;

public class ObjectWithAudioSource : MonoBehaviour {
    protected AudioSource audioSource;

    public void Start() {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    protected void PlayOneShot(AudioClip clip) {
        if(audioSource != null)
            audioSource.PlayOneShot(clip);
    }
    protected void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] Single volumeScale) {
        if(audioSource != null)
            audioSource.PlayOneShot(clip, volumeScale);
    }
}
