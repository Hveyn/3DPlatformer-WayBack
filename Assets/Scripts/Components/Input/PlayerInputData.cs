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
    }
    
    class InputPlayerTemplate : ComponentTemplate<PlayerInputData> { }
}