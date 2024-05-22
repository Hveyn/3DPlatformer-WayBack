using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Input {
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