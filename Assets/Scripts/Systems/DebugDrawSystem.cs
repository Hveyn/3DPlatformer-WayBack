using Components.Hover;
using DCFApixels.DragonECS;
using Services;
using UnityEngine;

namespace Systems
{
    /// <summary>
    /// Отображение данных для тестирования
    /// </summary>
    sealed class DebugDrawSystem: IEcsInit
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<HoverSettings> Settings;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        [EcsInject] private DebugDrawGizmosService drawGizmos;
        
        public void Init()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                drawGizmos.SetParametrs(a.Rb.Get(e).obj.transform, a.Settings.Get(e).settings);
            }
        }
    }
}