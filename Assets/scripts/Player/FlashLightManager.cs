using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour
{
    private GameObject lightObject;
    private Light myLight;
    private AudioManager audioManager;

    private float _currentTime = 0;

    private bool hasUsedFlashlightLastFrame;
    [SerializeField] private float debounce = 2f;
    
    void Start ()
    {
        lightObject = GameObject.Find("FlashLight");
        myLight = lightObject.GetComponent<Light>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    
    void Update ()
    {
        /*
        _currentTime += Time.deltaTime;

        if(InputHandler.HasUsedFlashlightThisFrame && _currentTime > debounce)
        {
            Debug.Log("space pressed");
            myLight.enabled = !myLight.enabled;
            // audioManager.Play("footstep");

            _currentTime = 0;
        }
        */
        if (!hasUsedFlashlightLastFrame && InputHandler.HasUsedFlashlightThisFrame) 
        {
            Debug.Log("space pressed");
            myLight.enabled = !myLight.enabled;
        }

        hasUsedFlashlightLastFrame = InputHandler.HasUsedFlashlightThisFrame;
    }
}
