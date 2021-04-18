using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int walkSpeed;
    [SerializeField] private int runSpeed;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    [SerializeField] private float minY = -60f;
    [SerializeField] private float maxY = 60f;

    [Header("References")] 
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator animator;
    
    public bool IsAudible = false;
    
    private int _moveSpeed;
 
    CharacterController controller;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        player = transform;

        _moveSpeed = walkSpeed;
    }
 
    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }
 
    void Look()
    {
        float lookY = Mathf.Clamp(inputHandler.LookY * sensitivityY, minY, maxY);
        player.localEulerAngles = new Vector3(0,inputHandler.LookX * sensitivityX, 0);
        _camera.localEulerAngles = new Vector3(lookY, 0, 0);
    }
 
    void Move()
    {
        if (inputHandler.HasRanThisFrame)
        {
            _moveSpeed = runSpeed;
            animator.SetTrigger("madeSound");
        } else {
             _moveSpeed = walkSpeed;
        }
        
        Vector3 moveDirection = transform.right * inputHandler.NextFrameMoveDirection.x + transform.forward * inputHandler.NextFrameMoveDirection.y;

        controller.Move(moveDirection * (_moveSpeed * Time.deltaTime));
    }
}
