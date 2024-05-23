using Components;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.Physics
{
    sealed  class PhysicsInitSystem: IEcsInit
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<RaycastHits> hits;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Init()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                a.hits.Get(e).Hits = new RaycastHit[10];
            }
        }
    }
}