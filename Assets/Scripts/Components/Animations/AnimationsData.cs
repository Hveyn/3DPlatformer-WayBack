using System;
using Components.Movement;
using DCFApixels.DragonECS;


namespace Components.Animations
{
    [Serializable]
    public struct AnimationsData: IEcsComponent
    {
        public int isRunningHash;
        public int isJumpingHash;
        public int yVelocityHash;
    }
    
    class AnimationTemplate: ComponentTemplate<AnimationsData> { }
}