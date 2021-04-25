using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMouseSens : MonoBehaviour
{
    public static SetMouseSens Instance;

    private Slider _slider;

    void Awake() 
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            if (_slider != null) {
                _slider = GetComponent<Slider>();
                _slider.value = MouseSensitivity.Instance.sensitivity;
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
    public void SetSensitivity(float input)
    {
        MouseSensitivity.Instance.sensitivity = input;
    }
}
