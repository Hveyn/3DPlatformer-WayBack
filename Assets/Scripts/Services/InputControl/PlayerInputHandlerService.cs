using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Mono.InputControl
{
    /// <summary>
    /// Обработчик ввода от игрока
    /// </summary>
    public class PlayerInputHandlerService : MonoBehaviour
    {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset playerConrols;
        
        [Header("Action Map Name References")] 
        [SerializeField] private string gameMapName = "Player";

        [Header("Action Name References")] 
        [SerializeField] private string move = "Move";
        [SerializeField] private string look = "Look";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string pause = "Pause";

        [Header("Deadzone Values")] 
        [SerializeField] private float leftStickDeadzoneValue = 0.2f;
    
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;
        private InputAction _pauseAction;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpTriggered { get; private set; }
        public float SprintValue { get; private set; }
        
        public bool PauseTriggered { get; private set; }

        public static PlayerInputHandlerService Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _moveAction = playerConrols.FindActionMap(gameMapName).FindAction(move);
            _lookAction = playerConrols.FindActionMap(gameMapName).FindAction(look);
            _jumpAction = playerConrols.FindActionMap(gameMapName).FindAction(jump);
            _sprintAction = playerConrols.FindActionMap(gameMapName).FindAction(sprint);
            _pauseAction = playerConrols.FindActionMap(gameMapName).FindAction(pause);
            RegisterInputActions();

            InputSystem.settings.defaultDeadzoneMin = leftStickDeadzoneValue;

            PrintDevices();
        }

        private void PrintDevices()
        {
            foreach (var device in InputSystem.devices)
            {
                if (device.enabled)
                {
                    Debug.Log("Active Device: " + device.name);
                }
            }
        }

        private void RegisterInputActions()
        {
            _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
            _moveAction.canceled += _ => MoveInput = Vector2.zero;
        
            _lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
            _lookAction.canceled += _ => LookInput = Vector2.zero;

            _jumpAction.performed += _ => JumpTriggered = true;
            _jumpAction.canceled += _ => JumpTriggered = false;
        
            _sprintAction.performed += context => SprintValue = context.ReadValue<float>();
            _sprintAction.canceled += _ => SprintValue = 0f;
        }

        private void Update()
        {
            PauseTriggered = _pauseAction.WasPressedThisFrame();
        }

        private void OnEnable()
        {
            _moveAction.Enable();
            _lookAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();
            _pauseAction.Enable();
            
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _jumpAction.Disable();
            _sprintAction.Disable();
            _pauseAction.Disable();
        
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange changeDevice)
        {
            switch (changeDevice)
            {
                case InputDeviceChange.Disconnected:
                    Debug.Log("Device Disconnected: "+device.name);
                    //Handle disconnection
                    break;
            
                case InputDeviceChange.Reconnected:
                    Debug.Log("Device Reconnected: "+device.name);
                    //Handle Reconnected
                    break;
            }
        }
    }
}
