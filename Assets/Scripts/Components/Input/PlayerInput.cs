using UnityEngine;

namespace Components {
    struct PlayerInput {
        
        
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool JumpTriggered { get; private set; }
        public float SprintValue { get; private set; }
    }
}