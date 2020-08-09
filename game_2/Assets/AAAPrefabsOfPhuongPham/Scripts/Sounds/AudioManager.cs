using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixerMaster;
    public AudioMixerGroup sfxGroups;
    public AudioMixerGroup themeGroups;

    public Sound[] soundsOfPlayer;
    public Sound[] soundsOfTheme;

    public float timeToLeaveCombatSound = 7f;
    Coroutine leaveCombatSound;

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
            s.source.spatialBlend = s.is3dSound;
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
        //Debug.Log(s.name);
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
        s.source.volume = 0;
        StartCoroutine(FadeIn(s));

        Debug.Log("play theme");

    }

    IEnumerator FadeIn(Sound s)
    {

        float i = 0;
        while (i < 10)
        {
            //Debug.Log(s.source.volume);
            yield return new WaitForSeconds(0.2f);

            i++;
            float x = (i / 10);
            s.source.volume = Mathf.Clamp(x, 0, s.volume);

            //Debug.Log(s.volume);

        }
    }

    IEnumerator FadeOut(Sound s)
    {

        float i = 10;
        while (i > 0)
        {
            //Debug.Log(s.source.volume);
            yield return new WaitForSeconds(0.2f);

            i--;
            float x = (i / 10);
            s.source.volume = Mathf.Clamp(x, 0, s.volume);

            //Debug.Log(s.volume);

        }
        s.source.Stop();
    }

    public void StopSoundOfTheme(string name)
    {
        /*
        Sound s = Array.Find(soundsOfTheme, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        */
        if (IsPlayTheme(name))
        {
            Sound s = Array.Find(soundsOfTheme, sound => sound.name == name);
            StartCoroutine(FadeOut(s));
            //s.source.Stop();
            Debug.Log("stop theme");
        }

    }

    public void SetVolumeSFX(Slider number)
    {
        audioMixerMaster.SetFloat("Volume SFX", number.value);
    }

    public void SetVolumeTheme(Slider number)
    {
        audioMixerMaster.SetFloat("Volume Theme", number.value);
    }

    public bool IsPlayTheme(string name)
    {
        Sound s = Array.Find(soundsOfTheme, sound => sound.name == name);
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

    public bool IsPlaySFX(string name)
    {
        Sound s = Array.Find(soundsOfPlayer, sound => sound.name == name);
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

    public void ResetCoroutineStopSoundTheme()
    {

        if (leaveCombatSound != null)
        {
            StopCoroutine(leaveCombatSound);
            leaveCombatSound = null;
        }
        leaveCombatSound = StartCoroutine(LeaveSoundCombat(timeToLeaveCombatSound));

    }

    public void StopCoroutineSoundTheme()
    {

        if (leaveCombatSound != null)
        {
            StopCoroutine(leaveCombatSound);
            leaveCombatSound = null;

        }
    }



    IEnumerator LeaveSoundCombat(float t)
    {
        yield return new WaitForSeconds(t);

        if (AudioManager.instance.IsPlayTheme("OnCombat"))
        {
            AudioManager.instance.StopSoundOfTheme("OnCombat");
        }

    }
}
