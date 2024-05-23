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
            [Inc] public EcsPool<UnityComponent<Rigidbody>> rb;
            [Inc] public EcsTagPool<HoverOnTag> tag;
            [Inc] public EcsTagPool<ApplyHoverForceTag> apply;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {

                a.SpringForce.Get(e).force -= UnityEngine.Physics.gravity;
                a.rb.Get(e).obj.AddForce(a.SpringForce.Get(e).force);
                if (a.CastResults.Get(e).hit.rigidbody)
                    a.CastResults.Get(e).hit.rigidbody.AddForceAtPosition(-a.SpringForce.Get(e).force, 
                        a.CastResults.Get(e).hit.point);
                
                a.SpringForce.Del(e);
                a.apply.Del(e);
                
            }
        }
        
    }
}