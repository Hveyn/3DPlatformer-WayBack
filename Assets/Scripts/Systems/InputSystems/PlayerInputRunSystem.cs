using DCFApixels.DragonECS;
using Components;
using Mono.InputControl;

namespace Client {
    sealed class PlayerInputRunSystem : IEcsRun
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> InputData;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        [EcsInject] private PlayerInputHandler inputHandler;
        
        public void Run ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                UnityDebugService.Activate();
                a.InputData.Get(e).moveInput = inputHandler.MoveInput;
                a.InputData.Get(e).lookInput = inputHandler.LookInput;
                a.InputData.Get(e).jumpTriggered = inputHandler.JumpTriggered;
                a.InputData.Get(e).sprintValue = inputHandler.SprintValue;
            }
        }
    }
}