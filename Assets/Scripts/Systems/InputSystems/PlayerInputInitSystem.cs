using Components;
using DCFApixels.DragonECS;
using Views;

namespace Client {
    sealed class PlayerInputInitSystem : IEcsInit
    {
        class Aspect : EcsAspect
        {
            public EcsPool<PlayerInputSettings> Settings = Inc;
        }

        [EcsInject] private EcsDefaultWorld _world;
        
        public void Init ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
               UnityDebugService.Activate();
               a.Settings.Get(e).Jump = "Jump";
               EcsDebug.Print(a.Settings.Get(e).Jump);
            }
        }
    }
}