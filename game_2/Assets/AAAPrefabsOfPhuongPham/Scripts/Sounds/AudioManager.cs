using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixerMaster;
    public AudioMixerGroup sfxGroups;
    public AudioMixerGroup themeGroups;

    public Sound[] soundsOfPlayer;
    public Sound[] soundsOfTheme;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in soundsOfPlayer)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = sfxGroups;
            //Debug.Log(audioMixerMaster.FindMatchingGroups("Master")[0]);
            //audioMixerVFX.outputAudioMixerGroup;
        }

        foreach (Sound s in soundsOfTheme)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = themeGroups;

        }
    }
    void Start()
    {
        //this.PlaySoundOfTheme("Theme Apocalypse");
    }



    public void PlaySoundOfPlayer(string name)
    {

        Sound s = Array.Find(soundsOfPlayer, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void PlaySoundOfTheme(string name)
    {

        Sound s = Array.Find(soundsOfTheme, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Play();
        Debug.Log("play theme");

    }

    public void SetVolumeSFX(Slider number)
    {
        audioMixerMaster.SetFloat("Volume SFX", number.value);
    }

    public void SetVolumeTheme(Slider number)
    {
        audioMixerMaster.SetFloat("Volume Theme", number.value);
    }
}
