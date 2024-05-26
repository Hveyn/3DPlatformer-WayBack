using Components.Animations;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.LocomotionSystems
{
    sealed class CharacterLocomotionSystem: IEcsRun
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<UnityComponent<Animator>> CharacterAnimator;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
            [Inc] public EcsPool<AnimationsData> Animations;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                a.CharacterAnimator.Get(e).obj.SetFloat(a.Animations.Get(e).isRunningHash,
                                                        a.Rb.Get(e).obj.velocity.magnitude);
                a.CharacterAnimator.Get(e).obj.SetFloat(a.Animations.Get(e).yVelocityHash,
                    a.Rb.Get(e).obj.velocity.y);
            }
        }
    }
}