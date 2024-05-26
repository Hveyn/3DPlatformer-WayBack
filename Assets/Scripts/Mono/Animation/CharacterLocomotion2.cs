using Mono.InputControl;
using Mono.Movement;
using UnityEngine;

namespace Mono.Animation
{
    public class CharacterLocomotion2 : MonoBehaviour
    {
        [Header("ModelAnimator")]
        [SerializeField] private Animator animator;

        [SerializeField] private PlayerMovementV3 testFallingJumping;

        private PlayerInputHandler _inputHandler;
        private int _isRunningHash;
        private int _isJumpingHash;
        private int _yVelocityHash;

        private bool _isJumpAnimating;
    
        private void Start()
        {
            _inputHandler = PlayerInputHandler.Instance;
            _isRunningHash = Animator.StringToHash("speed");
            _isJumpingHash = Animator.StringToHash("isJumping");
            _yVelocityHash = Animator.StringToHash("yVelocity");
        }

        private void Update()
        {
        
            animator.SetFloat(_isRunningHash, _inputHandler.MoveInput.magnitude);
            animator.SetFloat(_yVelocityHash, testFallingJumping.YVelocity);
            animator.SetBool(_isJumpingHash, testFallingJumping.IsJumping);
        
        }

    }
}
