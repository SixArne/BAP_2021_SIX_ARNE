using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int walkSpeed;
    [SerializeField] private int runSpeed;
    [SerializeField] private float minY = -60f;
    [SerializeField] private float maxY = 60f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float _groundDistance = 0.4f;

    [Header("References")] 
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Light _light;

    // Calculation variables
    private bool _isGrounded;
    
    public bool IsAudible = false;
    
    private int _moveSpeed;
 
    CharacterController controller;
    Transform player;

    Vector3 velocity;

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
        float lookY = Mathf.Clamp(InputHandler.LookY * MouseSensitivity.Instance.sensitivity, minY, maxY);
        player.localEulerAngles = new Vector3(0, InputHandler.LookX * MouseSensitivity.Instance.sensitivity, 0);
        _light.transform.localEulerAngles = new Vector3(lookY, 0, 0);
        _camera.localEulerAngles = new Vector3(lookY, 0, 0);
    }
 
    void Move()
    {
        GroundTheFuckingPlayer();

        if (InputHandler.HasRanThisFrame)
        {
            _moveSpeed = runSpeed;
            if (animator != null) animator.SetTrigger("madeSound");
        } else {
             _moveSpeed = walkSpeed;
        }
        
        Vector3 moveDirection = transform.right * InputHandler.NextFrameMoveDirection.x + transform.forward * InputHandler.NextFrameMoveDirection.y;

        controller.Move(moveDirection * (_moveSpeed * Time.deltaTime));

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void GroundTheFuckingPlayer()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
