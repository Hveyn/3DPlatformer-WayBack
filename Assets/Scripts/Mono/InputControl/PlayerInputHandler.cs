using UnityEngine;
using UnityEngine.InputSystem;

namespace Mono.InputControl
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset playerConrols;

        [Header("Action Map Name References")] 
        [SerializeField] private string actionMapName = "Player";

        [Header("Action Name References")] 
        [SerializeField] private string move = "Move";
        [SerializeField] private string look = "Look";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string sprint = "Sprint";

        [Header("Deadzone Values")] 
        [SerializeField] private float leftStickDeadzoneValue = 0.2f;
    
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _jumpAction;
        private InputAction _sprintAction;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpTriggered { get; private set; }
        public float SprintValue { get; private set; }

        public static PlayerInputHandler Instance { get; private set; }

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

            _moveAction = playerConrols.FindActionMap(actionMapName).FindAction(move);
            _lookAction = playerConrols.FindActionMap(actionMapName).FindAction(look);
            _jumpAction = playerConrols.FindActionMap(actionMapName).FindAction(jump);
            _sprintAction = playerConrols.FindActionMap(actionMapName).FindAction(sprint);
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

        private void OnEnable()
        {
            _moveAction.Enable();
            _lookAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();

            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void OnDisable()
        {
            _moveAction.Disable();
            _lookAction.Disable();
            _jumpAction.Disable();
            _sprintAction.Disable();
        
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
