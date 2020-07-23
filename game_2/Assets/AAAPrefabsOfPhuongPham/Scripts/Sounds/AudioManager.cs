using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;

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
        }

        foreach (Sound s in soundsOfTheme)
        {

            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        this.PlaySoundOfTheme("Theme Apocalypse");
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
        Debug.Log("play theme");
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
    }

    public void SetVolume(float number)
    {
        audioMixer.SetFloat("volume", number);
    }
}
