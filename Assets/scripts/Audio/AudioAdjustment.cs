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
        _slider = GetComponent<Slider>();
        _slider.value = defaultVolume;
        mixer.SetFloat("MusicVolume", Mathf.Log(defaultVolume) * 20);
    }

    public void SetLevel(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
}
