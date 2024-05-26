using Components.Animations;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.LocomotionSystems
{
    sealed class CharacterLocomotionInitSystem: IEcsInit
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<UnityComponent<Animator>> CharacterAnimator;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Init()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                EcsPool<AnimationsData> animations = _world.GetPool<AnimationsData>();

                if (!animations.Has(e))
                {
                    animations.Add(e);
                }
                
                animations.Get(e).isRunningHash = Animator.StringToHash("speed");
                animations.Get(e).isJumpingHash = Animator.StringToHash("isJumping");
                animations.Get(e).yVelocityHash = Animator.StringToHash("yVelocity");
            }
        }
    }
}