using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components {
    [Serializable]
    struct PlayerInput: IEcsComponent
    {
        public Vector2 MoveInput;
        public Vector2 LookInput;
        public bool JumpTriggered;
        public float SprintValue;
    }
    [Serializable]
    class InputPlayerTemplate : ComponentTemplate<PlayerInput> { }
}