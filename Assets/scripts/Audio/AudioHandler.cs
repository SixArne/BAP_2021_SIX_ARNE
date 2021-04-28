using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler INSTANCE;
    public AudioClip defaultAmbience;

    [SerializeField] private AudioSource track1, track2;
    private bool isPlayingTrack1;

    private AudioClip lastPlayedClip;
    
    private void Awake()
    {
        if (INSTANCE == null)
            INSTANCE = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        isPlayingTrack1 = true;
        
        SwapTrack(defaultAmbience);
    }

    public void Pause()
    {
        track1.Pause();
        track2.Pause();
    }

    public bool IsPlayingClip(AudioClip clip) {
        return (lastPlayedClip == clip);
    }

    public void Resume()
    {
        track1.Play();
        track2.Play();
    }

    public void SwapTrack(AudioClip newClip)
    {
        StopAllCoroutines();

        StartCoroutine(FadeTrack(newClip));

        isPlayingTrack1 = !isPlayingTrack1;
    }

    public void ReturnToDefault()
    {
        SwapTrack(defaultAmbience);
    }

    private IEnumerator FadeTrack(AudioClip newClip)
    {
        float timeToFade = 1.25f;
        float timeElapsed = 0;

        lastPlayedClip = newClip;
        
        if (isPlayingTrack1)
        {
            track2.clip = newClip;
            track2.Play();

            while (timeElapsed < timeToFade)
            {
                track2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                yield return null;

                timeElapsed += Time.deltaTime;
            }
            
            track1.Stop();
        }
        else
        {
            track1.clip = newClip;
            track1.Play();

            while (timeElapsed < timeToFade)
            {
                track1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                yield return null;

                timeElapsed += Time.deltaTime;
            }

            track2.Stop();
        }
    }
}
