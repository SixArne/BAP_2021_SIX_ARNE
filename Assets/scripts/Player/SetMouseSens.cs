using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMouseSens : MonoBehaviour
{
    private Slider _slider;

    void Awake() 
    {
        _slider = GetComponent<Slider>();
        _slider.value = MouseSensitivity.Instance.sensitivity;
        
        Debug.Log("Slider value");
        Debug.Log(_slider.value);
    }
    public void SetSensitivity(float input)
    {
        MouseSensitivity.Instance.sensitivity = input;
    }
}
