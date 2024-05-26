using Components.Movement;
using Components.Physics;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.MovementSystems.MoveSystems
{
    sealed class MoveSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<MovementComponent> Move;
            [Inc] public EcsPool<GroundCastResult> GroundCast;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Vector3 moveDirection = a.Move.Get(e).moveDirection;
                Vector3 goalVel = a.Move.Get(e).goalVel;
                float maxAccelForce = a.Move.Get(e).settings.maxAccelForce;
                float maxSpeed = a.Move.Get(e).settings.maxSpeed;
                float acceleration = a.Move.Get(e).settings.acceleration;
                AnimationCurve maxAccelerationForceFromDot = a.Move.Get(e).settings.maxAccelerationForceFromDot;
                Rigidbody rb = a.Rb.Get(e).obj;
                bool isGrounded = a.GroundCast.Get(e).resultCast;
                
                Vector3 unitVel = goalVel.normalized;

                float velDot = Vector3.Dot(moveDirection, unitVel);

                float accel = acceleration * maxAccelerationForceFromDot.Evaluate(velDot);
                
                float speedFactor = isGrounded ? 1f : a.Move.Get(e).settings.airMultiplier;
        
                Vector3 goalVelocity = moveDirection * (maxSpeed * speedFactor);

                goalVel = Vector3.MoveTowards(goalVel,
                    goalVelocity, accel * Time.deltaTime);
                
                Vector3 neededAccel = (goalVel - rb.velocity) / Time.deltaTime;
        
                float maxAccel = maxAccelForce * maxAccelerationForceFromDot.Evaluate(velDot);

                neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
                
                a.Move.Get(e).goalVel = goalVel;
                rb.AddForce(Vector3.Scale(neededAccel * rb.mass, a.Move.Get(e).settings.forceScale));
            }
        }
    }
}