using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] 
    private float _delay = 3.5f;

    [SerializeField]
    private AudioClip _doorOpen;
    
    [SerializeField]
    private AudioClip _doorClose;
    
    private AudioSource _audioSource;
    private Animator _doorAnimation;

    // Variables that are used for behaviour calculations
    private float _currentDelay = 0f;
    private bool _doorIsOpen = false;

    private void Awake()
    {
        _doorAnimation = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        _currentDelay += Time.deltaTime;
    }

    public void PlayAnimation()
    {
        if (_currentDelay >= _delay)
        {
            
            if (!_doorIsOpen)
            {
                _doorAnimation.Play("DoorOpen", 0, 0.0f);
                
                _audioSource.clip = _doorOpen;
                _audioSource.Play();
                
                _doorIsOpen = true;
            }
            else
            {
                _doorAnimation.Play("DoorClose", 0, 0.0f);

                _audioSource.clip = _doorClose;
                _audioSource.Play();
                
                _doorIsOpen = false;
            }

            _currentDelay = 0f;
        }
    }
}
