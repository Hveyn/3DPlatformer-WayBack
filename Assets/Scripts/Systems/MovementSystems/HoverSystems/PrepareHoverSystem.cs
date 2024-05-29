using Components;
using Components.Hover;
using Components.PhysicsComponents;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.MovementSystems
{
    sealed class PrepareHoverSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<HoverSettings> HoverSettings;
            [Inc] public EcsTagPool<HoverOnTag> tag;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> rb;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {

                EcsPool<GroundCastResult> cast = _world.GetPool<GroundCastResult>();

                if (!cast.Has(e))
                {
                    cast.Add(e);
                    
                    cast.Get(e).maxDistance = a.HoverSettings.Get(e).settings.maxDistance;
                    cast.Get(e).castRadius = a.HoverSettings.Get(e).settings.castRadius;
                    cast.Get(e).layers = a.HoverSettings.Get(e).settings.detectionLayers;
                }

                if (cast.Get(e).resultCast)
                {
                    EcsTagPool<ApplyHoverForceTag> applyForce = _world.GetTagPool<ApplyHoverForceTag>();
                    EcsPool<GetRelativeSpeedAlongDirection> relativeSpeed = _world.GetPool<GetRelativeSpeedAlongDirection>();
                    EcsPool<GetSpringForce> getSpringForce = _world.GetPool<GetSpringForce>();
                    
                    applyForce.Add(e);
                    relativeSpeed.Add(e);
                    getSpringForce.Add(e);

                    relativeSpeed.Get(e).targetBody = a.rb.Get(e).obj;
                    relativeSpeed.Get(e).frameBody = cast.Get(e).Hit.rigidbody;
                    relativeSpeed.Get(e).direction = Vector3.down;

                    getSpringForce.Get(e).height = a.HoverSettings.Get(e).settings.hoverHeight;
                    getSpringForce.Get(e).dampFactor = a.HoverSettings.Get(e).settings.dampFactor;
                    getSpringForce.Get(e).dampFrequency = a.HoverSettings.Get(e).settings.dampFrequency;
                    getSpringForce.Get(e).mass = a.rb.Get(e).obj.mass;
                    getSpringForce.Get(e).direction = Vector3.down;
                    
                }
            }
        }
        
    }
}