using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private KeyCode _runKey = KeyCode.LeftShift;
    [SerializeField] private float _walkSpeed = 12f;
    [SerializeField] private float _runSpeed = 25f;
    [SerializeField] private float _runPitch;
    [SerializeField] private float _walkPitch;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private Vector3 _velocity;
    
    [Header("References")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private AudioClip _walkingSound;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    // Calculation variables
    private bool _isGrounded;
    private Movement _movement;
    private float _currentSpeed;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.clip = _walkingSound;
    }

    // Update is called once per frame
    void Update()
    {
        DetectRunning();

        DetermineMovementPitch();

        HandleUserInput();
    }

    private void DetermineMovementPitch()
    {
        switch (_movement)
        {
            case Movement.RUNNING:
                _audioSource.pitch = _runPitch;
                PlayWalkingSound();
                break;
                
            case Movement.WALKING:
                _audioSource.pitch = _walkPitch;
                PlayWalkingSound();
                break;
            
            default:
                break;
        }
    }

    private void PlayWalkingSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    private void DetectRunning()
    {
        if (Input.GetKey(_runKey))
        {
            _movement = Movement.RUNNING;
            _currentSpeed = _runSpeed;
        }
        else if (IsMoving())
        {
            _movement = Movement.WALKING;
            _currentSpeed = _walkSpeed;
        }
        else
        {
            _movement = Movement.IDLE;
        }
    }

    private void HandleUserInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * _currentSpeed * Time.deltaTime);
        
        _velocity.y += gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        return _isGrounded;
    }

    private bool IsMoving()
    {
        return IsGrounded() && _controller.velocity.magnitude > 0;
    }
}

enum Movement
{
    RUNNING,
    WALKING,
    IDLE
}
