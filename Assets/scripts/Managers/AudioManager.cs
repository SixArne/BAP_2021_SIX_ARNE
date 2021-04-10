using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public String mainTheme;

    public Sound theme;

    [Header("Settings")] 
    [SerializeField] private float volumeIncreaseOnDetection = 5f;

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
        /*
        theme = Array.Find(sounds, sound => sound.name == mainTheme);
        
        theme.source.loop = true;
        theme.source.Play();
        */
    }

    void IncreaseThemeVolume()
    {
        theme.source.volume += volumeIncreaseOnDetection;
    }

    void DecreaseThemeVolume()
    {
        theme.source.volume -= volumeIncreaseOnDetection;
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
