using Components.Animations;
using Components.ComponentTags;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Systems.LocomotionSystems
{
    /// <summary>
    /// Система инициализации анимаций персонажа
    /// </summary>
    sealed class CharacterLocomotionInitSystem: IEcsRun
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<UnityComponent<Animator>> CharacterAnimator;
            [Inc] public EcsTagPool<PrepareAnimationTag> Tag;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run()
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
                a.Tag.Del(e);
            }
        }
    }
}