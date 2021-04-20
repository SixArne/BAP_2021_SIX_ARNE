using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioAdjustment : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private AudioMixer mixer;

    [Header("Settings")] 
    [SerializeField] private float defaultVolume = 0.5f;

    private Slider _slider;
    private void Awake()
    {
        float volume; 
        mixer.GetFloat("MusicVolume", out volume);

        _slider = GetComponent<Slider>();
        _slider.value = Mathf.Pow(10, volume);
    }

    public void SetLevel(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
}
