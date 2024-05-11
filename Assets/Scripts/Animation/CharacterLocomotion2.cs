using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterLocomotion2 : MonoBehaviour
{
    [Header("ModelAnimator")]
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerMovementV3 testFallingJumping;

    private PlayerInputHandler _inputHandler;
    private int _isRunningHash;
    private int _isJumpingHash;
    private int _isFallingHash;

    private bool _isJumpAnimating;
    
    private void Start()
    {
        _inputHandler = PlayerInputHandler.Instance;
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isFallingHash = Animator.StringToHash("isFalling");
    }

    private void Update()
    {
        if (_inputHandler.MoveInput != Vector2.zero)
        {
            animator.SetBool(_isRunningHash, true);
        }
        else
        {
            animator.SetBool(_isRunningHash, false);
        }

        animator.SetBool(_isJumpingHash,testFallingJumping.IsJumping);
        animator.SetBool(_isFallingHash,testFallingJumping.IsFalling);
    }

}
