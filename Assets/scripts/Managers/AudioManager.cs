using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public String mainTheme;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start()
    {
        Sound s = Array.Find(sounds, sound => sound.name == mainTheme);

        s.source.loop = true;
        s.source.Play();
    }

    public void Pause()
    {
        Sound s = Array.Find(sounds, sound => sound.name == mainTheme);

        s.source.Pause();
    }

    public void Resume()
    {
        Sound s = Array.Find(sounds, sound => sound.name == mainTheme);

        s.source.Play();
    }

    public void Play(string clipName) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == clipName);

        if (!s.source.isPlaying) 
        {
            if (PauseMenu.IsPaused)
            {
                s.source.pitch = 0f;
            }
            
            s.source.Play();
        }
    }
}
