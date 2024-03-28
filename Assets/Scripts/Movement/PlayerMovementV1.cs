
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")] 
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform orientation;
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private bool readyToJump = true;

    [Header("Ground Check")] 
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    
    private PlayerInputHandler _inputHandler;
    private Vector3 _moveDirection = Vector3.zero;
    private Rigidbody _rb;
    private bool _grounded;
    
    private void Awake()
    {
        _inputHandler = PlayerInputHandler.Instance;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        GroundCheck();
        SpeedControl();
    }
    
    private void FixedUpdate()
    {
        Move();
        HandleJump();

    }

    private void Move()
    {
        // calculate movement direction
        _moveDirection = orientation.forward * _inputHandler.MoveInput.y +
                         orientation.right * _inputHandler.MoveInput.x;
        
        if(_grounded) 
            _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        else if (!_grounded)
            _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
        
    }

    private void GroundCheck()
    {
        // ground check
        _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        // handle drag
        if (_grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;
        
        SpeedControl();
    }
    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void HandleJump()
    {
        if (_inputHandler.JumpTriggered && readyToJump && _grounded)
        {
            readyToJump = false;
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    
    private void Jump()
    {
        //reset y velocity
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
