using Components;
using Components.Hover;
using Components.Physics;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.MovementSystems
{
    sealed class ApplyHoverForceSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<GroundCastResult> CastResults;
            [Inc] public EcsPool<GetSpringForce> SpringForce;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
            [Inc] public EcsTagPool<HoverOnTag> OnTag;
            [Inc] public EcsTagPool<ApplyHoverForceTag> ApplyHoverForce;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Vector3 springForce = a.SpringForce.Get(e).force;
                RaycastHit hit = a.CastResults.Get(e).Hit;
                
                springForce -= UnityEngine.Physics.gravity;
                a.Rb.Get(e).obj.AddForce(springForce);
                if (hit.rigidbody) 
                    hit.rigidbody.AddForceAtPosition(-springForce, hit.point);
                
                a.SpringForce.Del(e);
                a.ApplyHoverForce.Del(e);
            }
        }
        
    }
}