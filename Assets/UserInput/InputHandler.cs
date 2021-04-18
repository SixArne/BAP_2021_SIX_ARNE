using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private bool _hasJumpedThisFrame;
    private bool _hasRanThisFrame;
    private bool _hasClickedThisFrame;

    private Vector2 _nextFrameMoveDirection;
    private float _lookX;
    private float _lookY;

    public bool HasJumpedThisFrame => _hasJumpedThisFrame;
    public bool HasRanThisFrame => _hasRanThisFrame;
    public Vector2 NextFrameMoveDirection => _nextFrameMoveDirection;
    public bool HasClickedThisFrame => _hasClickedThisFrame;
    public float LookX => _lookX;
    public float LookY => _lookY;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _nextFrameMoveDirection = context.ReadValue<Vector2>();
    }
 
    public void OnLook(InputAction.CallbackContext context)
    {
        _lookX += context.ReadValue<Vector2>().x * Time.deltaTime;
        _lookY -= context.ReadValue<Vector2>().y * Time.deltaTime;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        _hasClickedThisFrame = context.performed;
    }
 
    public void OnJump(InputAction.CallbackContext context)
    {
        _hasJumpedThisFrame = context.started;
    }
 
    public void OnRun(InputAction.CallbackContext context)
    {
        _hasRanThisFrame = context.performed;
    }
}
