using Components;
using DCFApixels.DragonECS;

namespace Client {
    sealed class PlayerInputDestroySystem : IEcsDestroy
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> InputData;
        }

        [EcsInject] private EcsDefaultWorld _world;
        [EcsInject] private InputSettingsScriptableObject settings;
        
        public void Destroy ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                EcsDebug.Print("DisableInput");
               a.InputData.Get(e).moveAction.Disable();
               a.InputData.Get(e).lookAction.Disable();
               a.InputData.Get(e).jumpAction.Disable();
               a.InputData.Get(e).sprintAction.Disable();
            }
        }
    }
}