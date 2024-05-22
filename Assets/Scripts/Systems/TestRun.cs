using DCFApixels.DragonECS;
using Components;
using Components.Input;

namespace Client {
    sealed class TestRun : IEcsRun
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> InputData;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                if (a.InputData.Get(e).jumpTriggered)
                {
                    UnityDebugService.Activate();
                    EcsDebug.Print("Jump");
                }
            }
        }
    }
}