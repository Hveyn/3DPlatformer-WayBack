using System;
using DCFApixels.DragonECS;
using SOData;

namespace Components.Movement
{
    [Serializable]
    public struct JumpComponent: IEcsComponent
    {
        public JumpSettingsSo settings;
        public float jumpPressedRemember;
        public float termVelocity;
        public float termTime;
        public float initJumpVelocity;
        public float gravity;
        public float cayoteTimeRemember;
        
        public bool isJumping;
        public bool isFalling;
    }
    
    class JumpTemplate: ComponentTemplate<JumpComponent> { }
}