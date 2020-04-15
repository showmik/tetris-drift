using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            //return;
        }

        DontDestroyOnLoad(gameObject);
        */

        SetAudio();

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        s.source.Play();
    }

    public void Pause()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Pause();
        }
    }

    public void UnPause()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.UnPause();
        }
    }

    public void SetAudio()
    {
        float volume = PlayerPrefs.GetFloat("MasterVolume", 0.3f);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.playOnAwake = false;
            sound.source.clip = sound.clip;
            sound.source.volume = volume;
            sound.source.pitch = sound.pitch;

            if (sound.name == "Theme")
            {
                sound.source.playOnAwake = true;
                sound.source.loop = true;
            }
        }
    }


}
