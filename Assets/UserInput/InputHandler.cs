using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler INSTANCE;

    public void Awake()
    {
        if (INSTANCE == null) INSTANCE = this;
        else Destroy(this.gameObject);
    }

    private bool _hasJumpedThisFrame;
    private bool _hasRanThisFrame;
    private bool _hasClickedThisFrame;
    private bool _hasPausedThisFrame;
    [SerializeField] private bool _hasUsedFlashlightThisFrame;
    private bool _hasOpenedMap;
    private Vector2 _nextFrameMoveDirection;
    private float _lookX;
    private float _lookY;

    public static bool HasJumpedThisFrame =>INSTANCE._hasJumpedThisFrame;
    public static bool HasRanThisFrame => INSTANCE._hasRanThisFrame;
    public static Vector2 NextFrameMoveDirection => INSTANCE._nextFrameMoveDirection;
    public static bool HasClickedThisFrame => INSTANCE._hasClickedThisFrame;
    public static bool HasPausedThisFrame => INSTANCE._hasPausedThisFrame;
    public static bool HasUsedFlashlightThisFrame => INSTANCE._hasUsedFlashlightThisFrame;
    public static bool HasOpenedMap => INSTANCE._hasOpenedMap;
    public static float LookX => INSTANCE._lookX;
    public static float LookY => INSTANCE._lookY;
    
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

    public void OnPause(InputAction.CallbackContext context) 
    {
        _hasPausedThisFrame = context.performed;
    }

    public void OnFlashlight(InputAction.CallbackContext context) 
    {
        _hasUsedFlashlightThisFrame = context.performed;
        Debug.Log("flash");
    }

    public void OnMap(InputAction.CallbackContext context) 
    {
        _hasUsedFlashlightThisFrame = context.performed;
    }
}
