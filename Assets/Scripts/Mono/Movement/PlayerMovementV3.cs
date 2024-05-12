using Mono.InputControl;
using Unity.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace Mono.Movement
{
    public class PlayerMovementV3 : MonoBehaviour
    {
   
        [Header("Movement settings")] 
        [SerializeField] private float maxSpeed = 3.0f;
        [SerializeField] private float acceleration = 200f; 
        [SerializeField] private float maxAccelForce = 150f; 
        [SerializeField] private AnimationCurve maxAccelerationForceFromDot;
        [SerializeField] private Vector3 forceScale;
        [SerializeField] private Transform orientation;
 
        [Header("Jump settings")]
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float minJumpHeight = 0.2f;
        [SerializeField] private float timeToApex = 0.5f;
        [SerializeField] private float jumpPressedRememberTime = 0.2f;
        [SerializeField] private float airMultiplier;
        [SerializeField, ReadOnly] private float initJumpVelocity;
        [SerializeField, ReadOnly] private float gravity;

    
        [Header("Hover settings")] 
        [SerializeField] private float dampFactor = 1;
        [SerializeField] private float dampFrequency = 15;
        [SerializeField] private float hoverHeight = 1.5f;
        [SerializeField] private float maxDistance = 2;
        [SerializeField] private float castRadius = .5f;
        [SerializeField] private LayerMask detectionLayers;
    
        [Header("Ground Check")] 
        [SerializeField] private float playerHeight = 1.7f;
        [SerializeField] private float heightOnGround = 1f;

        [Header("test VFX")] [SerializeField] private VisualEffect test;
    
        private PlayerInputHandler _inputHandler;
        private Rigidbody _rb;
        private RaycastHit[] _hits = new RaycastHit[10];

        private Vector3 _moveDirection = Vector3.zero;
        private Vector3 _goalVel = Vector3.zero;
    
        private float _jumpPressedRemember;
        private float _termVelocity;
        private float _termTime;
        private float _coyoutTime;

        private bool _hoverOn = true;
        private bool _isJumping;
        private bool _isFalling;
        private bool _grounded;

        /// <summary>
        /// тестовые переменные
        /// </summary>
        public bool IsJumping => _isJumping; 
        public float YVelocity => _rb.velocity.y; 
    
        private void Start()
        {
            _inputHandler = PlayerInputHandler.Instance;
            _rb = GetComponent<Rigidbody>();

            gravity = (2 * jumpHeight) / (timeToApex * timeToApex);
        
            Physics.gravity = new Vector3(0, -gravity, 0);

            initJumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            //termVelocity = math.sqrt(initJumpVelocity^2 + 2*g*(jumpHeight - minJumpHeight))
            _termVelocity = Mathf.Sqrt((initJumpVelocity * initJumpVelocity) + 2 * -gravity * (jumpHeight - minJumpHeight));
            Debug.Log($"_testVelocity: {_termVelocity}");
        
            //termTime = timeToApex - (2*(jumpHeight - minJumpHeight)/(initJumpVelocity + termVelocity))
            _termTime = timeToApex - (2 * (jumpHeight - minJumpHeight) / (initJumpVelocity + _termVelocity));
            Debug.Log($"_termTime: {_termTime}");
        }

        private void Update()
        {
            JumpHandler();
       
            if (_isFalling)
            {
            
                if (GroundCast(out RaycastHit hit))
                {
                    _isFalling = false;
                }
            }

            VFXControl();
        }

        private void FixedUpdate()
        {
            if (!_isFalling)
            {
                ApplyHoverForce();
            }
            Jump();
            Move();      
        
        }

        private void JumpHandler()
        {
            _jumpPressedRemember -= Time.deltaTime;

            if (_inputHandler.JumpTriggered && !_isJumping)
            {
                _jumpPressedRemember = jumpPressedRememberTime;
                _isJumping = true;
            }
            if (!_inputHandler.JumpTriggered && _isJumping)
            {
                Debug.Log("Yvelocity: " + _rb.velocity.y); 
            
                if (_rb.velocity.y > _termVelocity)
                {
                    Debug.Log("TermVelocity: " + _termVelocity); 

                    _rb.velocity = new Vector3(_rb.velocity.x, _termVelocity, _rb.velocity.z);
                }
                _isJumping = false;
                _isFalling = true;
            }
        }

        private void VFXControl()
        {
            if (test != null)
            {
                if (_grounded && _moveDirection != Vector3.zero)
                {
                    if (test.HasFloat("Duration"))
                    {
                        test.SetFloat("Duration",1f);
                    }
                }
                else
                {
                    if (test.HasFloat("Duration"))
                    {
                        test.SetFloat("Duration",0);
                    }
                }
            }
        }
    
        private void Jump()
        {
            if (_jumpPressedRemember > 0 && _grounded)
            {
                Debug.Log($"JumpVelocity: {initJumpVelocity}");
                _rb.velocity = new Vector3(_rb.velocity.x, initJumpVelocity, _rb.velocity.z);
            }
        }

        private void Move()
        {
            // calculate movement direction
            var move = orientation.forward * _inputHandler.MoveInput.y +
                       orientation.right * _inputHandler.MoveInput.x;

            /*if (_MovementControlDisabledTime > 0)
        {
            move = Vector3.zero;
            _MovementControlDisabledTime -= Time.deltaTime;
        }*/
        
            if (move.magnitude > 1.0f)
                move.Normalize();

            _moveDirection = move; //m_unitGoal

            Vector3 unitVel = _goalVel.normalized;

            float velDot = Vector3.Dot(_moveDirection, unitVel);

            float accel = acceleration * maxAccelerationForceFromDot.Evaluate(velDot);
        
            float speedFactor = _grounded ? 1f : airMultiplier;
        
            Vector3 goalVel = _moveDirection * maxSpeed * speedFactor;

            _goalVel = Vector3.MoveTowards(_goalVel,
                goalVel, accel * Time.deltaTime);
        
            // Move direction
            Debug.DrawLine(transform.position, transform.position + goalVel,
                Color.blue);


            Vector3 neededAccel = (_goalVel - _rb.velocity) / Time.deltaTime;
        
        
            float MaxAccel = maxAccelForce * maxAccelerationForceFromDot.Evaluate(velDot);

            neededAccel = Vector3.ClampMagnitude(neededAccel, MaxAccel);
        
            _rb.AddForce(Vector3.Scale(neededAccel * _rb.mass, forceScale));
        }
    
        void ApplyHoverForce()
        {
            if (GroundCast(out RaycastHit hit))
            {
                Vector3 rayDirection = Vector3.down;
                float springDelta = GetSpringDelta(hit);
                float springStrength = SpringStrength(_rb.mass, dampFrequency);
                float dampStrength = DampStrength(dampFactor, _rb.mass, dampFrequency);
                float springSpeed = GetRelativeSpeedAlongDirection(_rb, hit.rigidbody, rayDirection);
            
                Vector3 springForce = GetSpringForce(
                    springDelta, 
                    springSpeed, 
                    springStrength, 
                    dampStrength, 
                    rayDirection);
            
                springForce -= Physics.gravity;
                _rb.AddForce(springForce);
                if (hit.rigidbody) hit.rigidbody.AddForceAtPosition(-springForce, hit.point);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
        
            // Height On hover
            /*Gizmos.DrawLine(transform.position + new Vector3(0, -playerHeight/2, 0),
            transform.position + new Vector3(0,  -playerHeight/2 - 0.3f, 0));*/

            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position - transform.up * maxDistance, castRadius);
        }
    
        public bool GroundCast(out RaycastHit hit)
        {
            int hitCount = Physics.SphereCastNonAlloc(
                transform.position,
                castRadius,
                -transform.up,
                _hits,
                maxDistance,
                detectionLayers);
        
            Debug.DrawRay(transform.position, -transform.up, Color.red, maxDistance);
        
            if (hitCount > 0)
            {
                for (int i = 0; i < hitCount; i++)
                {
                    RaycastHit current = _hits[i];
                    if (current.rigidbody == _rb) continue;
                    hit = current;
                    _grounded = true;
                    return true;
                }
            }
            hit = default;
            _grounded = false;
            return false;
        }

        float GetSpringDelta(RaycastHit hit)
        {   
            // Hover Spring
            Debug.DrawLine(transform.position + new Vector3(1, 0, 0),
                transform.position + new Vector3(1, -hoverHeight-hoverHeight/2, 0),
                Color.yellow);
        
        
            return hit.distance - (hoverHeight - castRadius);
        }

        static float GetRelativeSpeedAlongDirection(
            Rigidbody targetBody, 
            Rigidbody frameBody, 
            Vector3 direction)
        {
            Vector3 velocity = targetBody.velocity;
            Vector3 hitBodyVelocity = frameBody ? frameBody.velocity : default;
            float rayDirectionSpeed = Vector3.Dot(direction, velocity);
            float hitBodyRayDirectionSpeed = Vector3.Dot(direction, hitBodyVelocity);
            return rayDirectionSpeed - hitBodyRayDirectionSpeed;
        }

        static float SpringStrength(float mass, float frequency)
        {
            return frequency * frequency * mass;
        }

        static float DampStrength(float dampFactor, float mass, float frequency)
        {
            float criticalDampStrength = 2 * mass * frequency;
            return dampFactor * criticalDampStrength;
        }

        static Vector3 GetSpringForce(
            float springDelta,
            float springSpeed,
            float springStrength,
            float dampStrength,
            Vector3 direction)
        {
            float tension = springDelta * springStrength;
            float damp = springSpeed * dampStrength;
            float forceMagnitude = tension - damp;
            Vector3 force = direction * forceMagnitude;
            return force;
        }
    }
}


