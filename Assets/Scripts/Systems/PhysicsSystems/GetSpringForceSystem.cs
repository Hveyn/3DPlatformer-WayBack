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
                float springDelta = a.Cast.Get(e).hit.distance -
                                    (a.GetForce.Get(e).height - a.Cast.Get(e).castRadius);
                
                float springStrength = a.GetForce.Get(e).dampFrequency * a.GetForce.Get(e).dampFrequency *
                                       a.GetForce.Get(e).mass;

                float criticalDampStrength = 2 * a.GetForce.Get(e).mass * a.GetForce.Get(e).dampFrequency;
                float dampStrength = a.GetForce.Get(e).dampFactor * criticalDampStrength;
                
                float tension = springDelta * springStrength;
                float damp = a.GetSpeed.Get(e).result * dampStrength;
                float forceMagnitude = tension - damp;
                a.GetForce.Get(e).force = a.GetForce.Get(e).direction * forceMagnitude;
                
                a.GetSpeed.Del(e);
            }
        }
    }
}