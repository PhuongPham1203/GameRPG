using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioEnemy : MonoBehaviour
{


    public AudioMixerGroup sfxGroups;

    public Sound[] soundOfEnemy;

    private void Awake()
    {
        foreach(Sound s in soundOfEnemy)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = sfxGroups;
            s.source.spatialBlend = s.is3dSound;

        }
    }

    public void PlaySoundOfEnemy(string name)
    {
        Sound s = Array.Find(soundOfEnemy, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Play();
    }
    public bool IsPlaySFX(string name)
    {
        Sound s = Array.Find(soundOfEnemy, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound in IsPlay " + name + " not found!");
            return false;
        }

        if (s.source.isPlaying)
        {
            return true;
        }

        return false;
    }
}
