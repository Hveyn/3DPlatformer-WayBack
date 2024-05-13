using System;
using DCFApixels.DragonECS;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    [Serializable]
    struct PlayerInputSettings: IEcsComponent
    {
        //Input Action Asset
        public InputActionAsset PlayerControls;

        // Action Map Name References"
        public string ActionMapName;

        //Action Name References
        public string Move;
        public string Look;
        public string Jump;
        public string Sprint;

        //Deadzone Values
        public float LeftStickDeadzoneValue;
    }

    [Serializable]
    class PlayerInputSettingsTemplate : ComponentTemplate<PlayerInputSettings>
    {
        [SerializeField]
        protected PlayerInputSettings inputSettings;
        public override Type Type { get { return typeof(PlayerInputSettings); } }
        public override void Apply(short worldID, int entityID)
        {
            EcsWorld.GetPoolInstance<EcsPool<PlayerInputSettings>>(worldID).TryAddOrGet(entityID)=component;
        }
        public override object GetRaw() { return inputSettings; }
        public override void SetRaw(object raw) { inputSettings = (PlayerInputSettings)raw; }
        public override void OnGizmos(Transform transform, IComponentTemplate.GizmosMode mode) { /*...*/ }
        public override void OnValidate(UnityEngine.Object obj) { /*...*/ }
        
    }
}