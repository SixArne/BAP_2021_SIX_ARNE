using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private string _volumeParameter;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] [Range(1, 10)] private float _multiplier;

    private static AudioController INSTANCE;
    
    private void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
        else Destroy(this.gameObject);
    }

    public static void IncreaseVolume()
    {
        INSTANCE._IncreaseVolume();
    }

    public static void DecreaseVolume()
    {
        INSTANCE._DecreaseVolume();
    }

    private void _IncreaseVolume()
    {
        float oldVolume;

        INSTANCE._mixer.GetFloat(_volumeParameter, out oldVolume);
        INSTANCE._mixer.SetFloat(_volumeParameter, oldVolume += _multiplier);
    }

    private void _DecreaseVolume()
    {
        float oldVolume;

        INSTANCE._mixer.GetFloat(_volumeParameter, out oldVolume);
        INSTANCE._mixer.SetFloat(_volumeParameter, oldVolume -= _multiplier);
    }
}
