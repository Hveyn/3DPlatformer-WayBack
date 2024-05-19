using DCFApixels.DragonECS;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Components.PlayerInput;

namespace Client {
    sealed class PlayerInputInitSystem : IEcsInit
    {
        class Aspect : EcsAspect
        {
            public EcsPool<PlayerInput> InputData = Inc;
        }

        [EcsInject] private EcsDefaultWorld _world;
        [EcsInject] private InputSettingsScriptableObject settings;
        
        public void Init ()
        {
            foreach (var e in _world.Where(out Aspect a))
            { 
                EcsDebug.Print("InputSettings");
               a.InputData.Get(e).moveAction =
                   settings.playerControls.FindActionMap(settings.actionMapName).FindAction(settings.move);
               
               a.InputData.Get(e).moveAction =
                   settings.playerControls.FindActionMap(settings.actionMapName).FindAction(settings.look);
               
               a.InputData.Get(e).moveAction =
                   settings.playerControls.FindActionMap(settings.actionMapName).FindAction(settings.jump);
               
               a.InputData.Get(e).moveAction =
                   settings.playerControls.FindActionMap(settings.actionMapName).FindAction(settings.sprint);

               InputSystem.settings.defaultDeadzoneMin = settings.leftStickDeadzoneValue;
               
               
               a.InputData.Get(e).moveAction.performed += context => a.InputData.Get(e).moveInput = context.ReadValue<Vector2>();
               a.InputData.Get(e).moveAction.canceled += _ => a.InputData.Get(e).moveInput = Vector2.zero;
        
               a.InputData.Get(e).lookAction.performed += context => a.InputData.Get(e).lookInput = context.ReadValue<Vector2>();
               a.InputData.Get(e).lookAction.canceled += _ => a.InputData.Get(e).lookInput = Vector2.zero;

               a.InputData.Get(e).jumpAction.performed += _ => a.InputData.Get(e).jumpTriggered = true;
               a.InputData.Get(e).jumpAction.canceled += _ => a.InputData.Get(e).jumpTriggered = false;
        
               a.InputData.Get(e).sprintAction.performed += context => a.InputData.Get(e).sprintValue = context.ReadValue<float>();
               a.InputData.Get(e).sprintAction.canceled += _ => a.InputData.Get(e).sprintValue = 0f;
               
               a.InputData.Get(e).moveAction.Enable();
               a.InputData.Get(e).lookAction.Enable();
               a.InputData.Get(e).jumpAction.Enable();
               a.InputData.Get(e).sprintAction.Enable();
            }
        }
    }
}