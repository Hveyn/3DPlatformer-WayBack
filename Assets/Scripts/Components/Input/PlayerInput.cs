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
    class InputPlayerTemplate : ComponentTemplate<PlayerInput>
    {
        [SerializeField]
        protected PlayerInput input;
        public override Type Type { get { return typeof(PlayerInput); } }
        public override void Apply(short worldID, int entityID)
        {
            EcsWorld.GetPoolInstance<EcsPool<PlayerInput>>(worldID).TryAddOrGet(entityID) = component;
        }
        public override object GetRaw() { return input; }
        public override void SetRaw(object raw) { input = (PlayerInput)raw; }
        public override void OnGizmos(Transform transform, IComponentTemplate.GizmosMode mode) { /*...*/ }
        public override void OnValidate(UnityEngine.Object obj) { /*...*/ }
    }
}