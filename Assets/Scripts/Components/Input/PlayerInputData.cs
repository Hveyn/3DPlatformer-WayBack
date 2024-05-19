using System;
using DCFApixels.DragonECS;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components {
    [Serializable]
    struct PlayerInputData: IEcsComponent
    {
        [Header("Inputs Data")]
        public Vector2 moveInput;
        public Vector2 lookInput;
        public bool jumpTriggered;
        public float sprintValue;
        
        
        public InputAction moveAction;
        
        public InputAction lookAction;
        [HideInInspector]
        public InputAction jumpAction;
        [HideInInspector]
        public InputAction sprintAction;
    }
    
    class InputPlayerTemplate : ComponentTemplate<PlayerInputData> { }
}