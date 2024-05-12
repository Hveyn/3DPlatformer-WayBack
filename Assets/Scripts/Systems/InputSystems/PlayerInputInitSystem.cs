using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Views;

namespace Client {
    sealed class PlayerInputInitSystem : IEcsInitSystem
    {

        readonly EcsCustomInject<PlayerInputSettingsView> _playerInputSettingsView = default;
        
        public void Init (IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsPool<PlayerInput> playerInputPool = world.GetPool<PlayerInput>();
            EcsPool<PlayerInputSettings> inputSettingsPool = world.GetPool<PlayerInputSettings>();
            
            int inputEntity = world.NewEntity();

            ref PlayerInput playerInput = ref playerInputPool.Add(inputEntity);
            ref PlayerInputSettings inputSettings = ref inputSettingsPool.Add(inputEntity);

            inputSettings.PlayerControls = _playerInputSettingsView.Value.playerConrols;
            inputSettings.ActionMapName = _playerInputSettingsView.Value.actionMapName;
            inputSettings.Move = _playerInputSettingsView.Value.move;
            inputSettings.Look = _playerInputSettingsView.Value.look;
            inputSettings.Jump = _playerInputSettingsView.Value.jump;
            inputSettings.Sprint = _playerInputSettingsView.Value.sprint;
            inputSettings.LeftStickDeadzoneValue = _playerInputSettingsView.Value.leftStickDeadzoneValue;
        }
    }
}