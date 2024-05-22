using Components;
using Components.Hover;
using Components.Physics;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.MovementSystems
{
    sealed class PrepareHoverSystem: IEcsRun
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<HoverData> HoverDatas;
            [Inc] public EcsPool<GroundCastResult> CastResults;
            [Inc] public EcsTagPool<HoverOnTag> tag;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> rb;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                if (a.CastResults.Get(e).resultCast)
                {
                    EcsTagPool<ApplyHoverForceTag> applyForce = _world.GetTagPool<ApplyHoverForceTag>();
                    EcsPool<GetRelativeSpeedAlongDirection> relativeSpeed = _world.GetPool<GetRelativeSpeedAlongDirection>();
                    EcsPool<GetSpringForce> getSpringForce = _world.GetPool<GetSpringForce>();
                    
                    applyForce.Add(e);
                    relativeSpeed.Add(e);
                    getSpringForce.Add(e);

                    relativeSpeed.Get(e).frameBody = a.rb.Get(e).obj;
                    relativeSpeed.Get(e).frameBody = a.CastResults.Get(e).hit.rigidbody;
                    relativeSpeed.Get(e).direction = Vector3.down;
                    
                    getSpringForce.Get(e).springDelta = 
                        a.CastResults.Get(e).hit.distance - 
                                    (a.HoverDatas.Get(e).settings.hoverHeight - a.HoverDatas.Get(e).settings.castRadius);
                    
                    
                }
            }
        }
        
    }
}