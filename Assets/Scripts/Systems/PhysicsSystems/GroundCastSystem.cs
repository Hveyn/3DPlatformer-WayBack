using DCFApixels.DragonECS;
using Components;
using Components.Physics;
using UnityEngine;

namespace Client.Physics
{
    sealed class GroundCastSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<RaycastHits> Hits;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
            [Inc] public EcsPool<GroundCastResult> CastResult;
        }

        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                UnityDebugService.Activate();
                var someMarker = new EcsProfilerMarker("GroundCastSystem");
                someMarker.Begin();
                
                int hitCount = UnityEngine.Physics.SphereCastNonAlloc(
                    a.Rb.Get(e).obj.transform.position,
                    a.CastResult.Get(e).castRadius,
                    -a.Rb.Get(e).obj.transform.up,
                    a.Hits.Get(e).Hits,
                    a.CastResult.Get(e).maxDistance,
                    a.CastResult.Get(e).layers);

                if (hitCount > 0)
                {
                    for (int i = 0; i < hitCount; i++)
                    {
                        RaycastHit current =  a.Hits.Get(e).Hits[i];
                        if (current.rigidbody == a.Rb.Get(e).obj) continue;
                        a.CastResult.Get(e).hit = current;
                        a.CastResult.Get(e).resultCast = true;
                    }
                }
                else
                {
                    a.CastResult.Get(e).hit = default;
                    a.CastResult.Get(e).resultCast = false;
                }
                someMarker.End();
            }
        }
    }
}