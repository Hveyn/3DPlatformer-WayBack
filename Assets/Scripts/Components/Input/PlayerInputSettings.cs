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
    class PlayerInputSettingsTemplate : ComponentTemplate<PlayerInputSettings> { }
}