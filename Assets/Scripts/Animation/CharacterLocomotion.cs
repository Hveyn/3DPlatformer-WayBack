using Unity.Mathematics;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [Header("ModelAnimator")]
    [SerializeField] private Animator animator;

    private PlayerInputHandler _inputHandler;
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isJumpingHash;

    private bool _isJumpAnimating;
    
    private void Start()
    {
        _inputHandler = PlayerInputHandler.Instance;
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
    }

    private void Update()
    {
        if (_inputHandler.MoveInput != Vector2.zero)
        {
            animator.SetBool(_isWalkingHash, true);
            
            if (math.abs(_inputHandler.MoveInput.x) > 0.9f
                || math.abs(_inputHandler.MoveInput.y) > 0.9f)
            {
                animator.SetBool(_isRunningHash, true);
            }
            
        }
        else
        {
            animator.SetBool(_isWalkingHash, false);
            animator.SetBool(_isRunningHash, false);
        }
    }

    public void StartJumping()
    {
        animator.SetBool(_isJumpingHash,true);
        _isJumpAnimating = true;
    }
    
    public void EndJumping()
    {
        if (_isJumpAnimating)
        {
            animator.SetBool(_isJumpingHash, false);
            _isJumpAnimating = false;
        }
    }
}
