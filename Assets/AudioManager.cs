using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Hellmade.Sound;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public AudioMixerGroup master, sfx, bgm, ui;
    public float attenuationDistance;

    private Transform listener;

    public Transform Listener
    {
        get
        {
            if (listener == null)
            {
                listener = Player.Instance.transform;
            }
            return listener;
        }
    }

    public void Play(AudioPlayer player)
    {
        var volume = player.volume;
        if (player.isAttenuate)
        {
            var distance = (player.transform.position - Listener.position).magnitude;
            volume *= Mathf.Clamp01(1f - distance / attenuationDistance);
            //Debug.Log($"[Audio] {volume}");
        }

        switch (player.type)
        {
            case AudioPlayer.AudioMixerType.SFX:
                EazySoundManager.PlaySound(player.clip, volume);
                return;

            case AudioPlayer.AudioMixerType.UI:
                EazySoundManager.PlayUISound(player.clip, volume);
                return;

            case AudioPlayer.AudioMixerType.BGM:
                EazySoundManager.PlayMusic(player.clip, volume, true, true);
                return;
        }
    }

    public AudioMixerGroup AudioMixer(Audio.AudioType audioType)
    {
        
        switch(audioType)
        {
            case Audio.AudioType.Sound:
                return sfx;
            case Audio.AudioType.UISound:
                return ui;
            case Audio.AudioType.Music:
                return bgm;
            default:
                return master;
        }
    }
}
