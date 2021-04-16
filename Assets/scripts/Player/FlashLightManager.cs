using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightManager : MonoBehaviour
{
    private GameObject lightObject;
    private Light myLight;
    private AudioManager audioManager;
    
    void Start ()
    {
        lightObject = GameObject.Find("FlashLight");
        myLight = lightObject.GetComponent<Light>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    
    void Update ()
    {
        /*
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("space pressed");
            myLight.enabled = !myLight.enabled;
            audioManager.Play("footstep");
        }
        */
    }
}
