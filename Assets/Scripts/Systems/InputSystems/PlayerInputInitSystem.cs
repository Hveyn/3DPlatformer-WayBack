using Components;
using DCFApixels.DragonECS;
using Views;

namespace Client {
    sealed class PlayerInputInitSystem : IEcsInit
    {
        [EcsInject] private EcsDefaultWorld _world;
        [EcsInject] private PlayerInputSettingsView _view;
        
        public void Init ()
        {
            int inputEntity = _world.NewEntity();
            
            EcsPool<PlayerInput> playerInputPool = _world.GetPool<PlayerInput>();
            EcsPool<PlayerInputSettings> inputSettingsPool = _world.GetPool<PlayerInputSettings>();
            
            ref PlayerInput playerInput = ref playerInputPool.Add(inputEntity);
            ref PlayerInputSettings inputSettings = ref inputSettingsPool.Add(inputEntity);

            inputSettings.PlayerControls = _view.playerConrols;
            inputSettings.ActionMapName = _view.actionMapName;
            inputSettings.Move = _view.move;
            inputSettings.Look = _view.look;
            inputSettings.Jump = _view.jump;
            inputSettings.Sprint = _view.sprint;
            inputSettings.LeftStickDeadzoneValue = _view.leftStickDeadzoneValue;
        }
    }
}