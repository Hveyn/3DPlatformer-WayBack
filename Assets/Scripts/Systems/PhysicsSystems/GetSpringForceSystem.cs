using DCFApixels.DragonECS;
using Components;
using Components.Physics;
using UnityEngine;

namespace Client.Physics
{
    sealed class GetSpringForceSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<GetRelativeSpeedAlongDirection> GetSpeed;
            [Inc] public EcsPool<GetSpringForce> GetForce;
            [Inc] public EcsPool<GroundCastResult> Cast;
        }

        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                float dampFrequency = a.GetForce.Get(e).dampFrequency;
                float dampFactor = a.GetForce.Get(e).dampFactor;
                float springSpeed = a.GetSpeed.Get(e).relativeSpeed;
                float height = a.GetForce.Get(e).height;
                float mass = a.GetForce.Get(e).mass;
                float distance = a.Cast.Get(e).hit.distance;
                float castRadius = a.Cast.Get(e).castRadius;
                
                Vector3 direction = a.GetForce.Get(e).direction;
                
                float springDelta = distance - (height - castRadius);
                
                float springStrength = dampFrequency * dampFrequency * mass;

                float criticalDampStrength = 2 * mass * dampFrequency;
                float dampStrength = dampFactor * criticalDampStrength;
                
                float tension = springDelta * springStrength;
                float damp = springSpeed * dampStrength;
                float forceMagnitude = tension - damp;
                a.GetForce.Get(e).force = direction * forceMagnitude;
                
                a.GetSpeed.Del(e);
            }
        }
    }
}