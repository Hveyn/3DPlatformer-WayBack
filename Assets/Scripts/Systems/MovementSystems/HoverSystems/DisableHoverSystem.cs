using Components.Hover;
using Components.PhysicsComponents;
using DCFApixels.DragonECS;

namespace Systems.MovementSystems.HoverSystems
{
    /// <summary>
    /// Система отключающая HoverSytems
    /// </summary>
    sealed class DisableHoverSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<HoverSettings> Settings;
            [Inc] public EcsPool<GroundCastResult> CastResult;
            [Exc] public EcsTagPool<HoverOnTag> OnTag;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                a.CastResult.Del(e);
            }
        }
        
    }
}