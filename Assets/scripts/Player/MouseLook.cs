using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Settings")]
    public float mouseSensitivity = 100f;
    
    [Header("References")]
    public Transform playerBody;
    public Transform flashLight;
    
    private float _xRotation = 0f;
    private float _mouseX = 0.5f;
    private float _mouseY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse Y is 0 FIX THIS

        _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= _mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        flashLight.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        
        playerBody.Rotate(Vector3.up * _mouseX);
    }
}
