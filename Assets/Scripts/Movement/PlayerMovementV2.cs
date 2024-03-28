using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float groundDrag;
    [SerializeField] private Transform orientation;
   
    [Header("Jump Parameters")]
    [SerializeField, ReadOnly] private float _initialJumpVelocity;
    [SerializeField] private float _maxJumpHeight = 1.0f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    [SerializeField] private float airMultiplier;

    [Header("Gravity Parameters")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float groundedGravity = -0.05f;
    
    private bool _isJumping;
    
    private CharacterController _characterController;
    private PlayerInputHandler _inputHandler;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _currentMovement = Vector3.zero;
    private Vector3 _appliedMovement = Vector3.zero;

    private CharacterLocomotion _locomotion;

    private void Awake()
    {
        _inputHandler = PlayerInputHandler.Instance;
        _characterController = GetComponent<CharacterController>();
        _locomotion = GetComponent<CharacterLocomotion>();
        _characterController.detectCollisions = true;
        SetupJumpVariables();
    }

    private void Update()
    {
        Move();
        HandleGravity();
        HandleJump();
    }

    private void Move()
    {
        // calculate movement direction

        _moveDirection = orientation.forward * _inputHandler.MoveInput.y +
                         orientation.right * _inputHandler.MoveInput.x;
        
        _currentMovement = new Vector3(_moveDirection.x, _currentMovement.y, _moveDirection.z);

        _appliedMovement = new Vector3(_currentMovement.x, _appliedMovement.y, _currentMovement.z);
        _characterController.Move(_appliedMovement * (moveSpeed * Time.deltaTime));
    }

    private void HandleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f || !_inputHandler.JumpTriggered;
        float fallMultiplier = 2.0f;
        
        if (_characterController.isGrounded)
        {
            
            _locomotion.EndJumping();
            _currentMovement.y = groundedGravity;
            _appliedMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y += (gravity * fallMultiplier * Time.deltaTime);
            _appliedMovement.y = Mathf.Max(previousYVelocity + _currentMovement.y * .5f, -20.0f);
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y += (gravity * Time.deltaTime);
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
        }
    }

    private void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    private void HandleJump()
    {
        if (!_isJumping && _characterController.isGrounded && _inputHandler.JumpTriggered)
        {
            _locomotion.StartJumping();
            _isJumping = true;
            _appliedMovement.y = _initialJumpVelocity;
            _currentMovement.y = _initialJumpVelocity;
        }
        else if (!_inputHandler.JumpTriggered && _isJumping && _characterController.isGrounded)
        {
            _isJumping = false;
        }
    }
}
