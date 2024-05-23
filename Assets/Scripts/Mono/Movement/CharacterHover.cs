using UnityEngine;

namespace Mono.Movement
{
    public class CharacterHover : MonoBehaviour
    {
        [Header("Hover settings")] 
        [SerializeField] private float dampFactor = 1;
        [SerializeField] private float dampFrequency = 15;
        [SerializeField] private float hoverHeight = 1.5f;
        [SerializeField] private float maxDistance = 2;
        [SerializeField] private float castRadius = .5f;  
    
        private Rigidbody _rb;
        private RaycastHit[] _hits = new RaycastHit[10];

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        } 

        private void FixedUpdate()
        {
            ApplyHoverForce();
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

        public bool GroundCast(out RaycastHit hit)
        {
            int hitCount = Physics.SphereCastNonAlloc(
                transform.position,
                castRadius,
                -transform.up,
                _hits,
                maxDistance);
        
            if (hitCount > 0)
            {
                for (int i = 0; i < hitCount; i++)
                {
                    RaycastHit current = _hits[i];
                    if (current.rigidbody == _rb) continue;
                    hit = current;
                    return true;
                }
            }
            hit = default;
            return false;
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

        float GetSpringDelta(RaycastHit hit)
        {
            return hit.distance - (hoverHeight - castRadius);
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

