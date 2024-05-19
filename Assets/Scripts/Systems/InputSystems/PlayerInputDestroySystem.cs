using DCFApixels.DragonECS;
using PlayerInput = Components.PlayerInput;

namespace Client {
    sealed class PlayerInputDestroySystem : IEcsDestroy
    {
        class Aspect : EcsAspect
        {
            public EcsPool<PlayerInput> InputData = Inc;
        }

        private EcsDefaultWorld _world;
        private InputSettingsScriptableObject settings;
        
        public void Destroy ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
               a.InputData.Get(e).moveAction.Disable();
               a.InputData.Get(e).lookAction.Disable();
               a.InputData.Get(e).jumpAction.Disable();
               a.InputData.Get(e).sprintAction.Disable();
            }
        }
    }
}