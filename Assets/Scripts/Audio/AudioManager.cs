using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] BGsounds, SEsounds;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }else
        {
            Instance = this;
        }

        foreach(Sound s in BGsounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = true;
            s.source.volume = 0.5f;
            s.source.playOnAwake = false;
        }

        foreach (Sound s in SEsounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = false;
            s.source.volume = 1f;
            s.source.playOnAwake = false;
        }

    }
    
    public void PlayBG(string audioName)
    {
        Sound s = Array.Find(BGsounds, sound => sound.name.Equals(audioName));
        if (s != null && !s.source.isPlaying)
            s.source.Play();
    }

    public void StopBG(string audioName)
    {
        Sound s = Array.Find(BGsounds, sound => sound.name.Equals(audioName));
        if (s != null && s.source.isPlaying)
            s.source.Stop();
    }

    public void PlaySE(string audioName)
    {
        Sound s = Array.Find(SEsounds, sound => sound.name.Equals(audioName));
        if(s != null && !s.source.isPlaying)
            s.source.Play();
    }

    public void GetSettingAudio(JsonPlayerPrefs jsonPref)
    {
        float master = (float)jsonPref.GetInt("settingmaster");
        float bgm = (float)jsonPref.GetInt("settingbgm");
        float sfx = (float)jsonPref.GetInt("settingsfx");

        SettingAudio(master, bgm, sfx);
    }

    public void SettingAudio(float master, float bgm, float sfx)
    {
        foreach (Sound s in BGsounds)
        {
            s.source.volume = master * (bgm / 20000);
        }
        foreach (Sound s in SEsounds)
        {
            s.source.volume = master * (sfx / 10000);
        }
    }
}
