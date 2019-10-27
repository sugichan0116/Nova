using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AudioPlayer : MonoBehaviour
{
    public enum AudioMixerType {
        SFX,
        UI,
        BGM=10,
    }


    public AudioClip clip;
    public AudioMixerType type;
    [Slider(0, 1)]
    public float volume = 1f;
    public bool isAttenuate;

    public void Play()
    {
        AudioManager.Instance.Play(this);
    }
}
