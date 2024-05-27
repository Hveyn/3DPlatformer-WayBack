using DCFApixels.DragonECS;
using Components;
using Components.Input;
using Mono.InputControl;

namespace Client {
    sealed class PlayerInputRunSystem : IEcsRun
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> InputData;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        [EcsInject] private PlayerInputHandlerService inputHandlerService;
        
        public void Run ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                UnityDebugService.Activate();
                a.InputData.Get(e).moveInput = inputHandlerService.MoveInput;
                a.InputData.Get(e).lookInput = inputHandlerService.LookInput;
                a.InputData.Get(e).jumpTriggered = inputHandlerService.JumpTriggered;
                a.InputData.Get(e).sprintValue = inputHandlerService.SprintValue;
            }
        }
    }
}