using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensitivity : MonoBehaviour
{
    public static MouseSensitivity Instance;
    public float sensitivity = 3;

   void Awake() 
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
}
