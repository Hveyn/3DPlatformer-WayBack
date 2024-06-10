using Components.ComponentTags;
using Components.PhysicsComponents;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Systems.PhysicsSystems
{
    /// <summary>
    /// Инициализация физических элементов
    /// </summary>
    sealed class PhysicsInitSystem: IEcsRun
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<RaycastHits> Hits;
            [Inc] public EcsTagPool<PreparePhysicsTag> Tag;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                a.Hits.Get(e).Hits = new RaycastHit[10];
                a.Tag.Del(e);
            }
        }
    }
}