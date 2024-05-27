using Components.Input;
using Components.Movement;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Systems.MovementSystems.JumpSystems
{
    sealed class JumpHandlerSystem: IEcsRun
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> PlayerInput;
            [Inc] public EcsPool<JumpComponent> JumpData;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                float termVelocity = a.JumpData.Get(e).termVelocity;
                Rigidbody rb = a.Rb.Get(e).obj;

                a.JumpData.Get(e).jumpPressedRemember -= Time.deltaTime;
                
                if (a.PlayerInput.Get(e).jumpTriggered && !a.JumpData.Get(e).isJumping)
                {
                    a.JumpData.Get(e).jumpPressedRemember = a.JumpData.Get(e).settings.jumpPressedRememberTime;
                    a.JumpData.Get(e).isJumping = true;
                }
                if (!a.PlayerInput.Get(e).jumpTriggered && a.JumpData.Get(e).isJumping)
                {
                    //Debug.Log("Yvelocity: " + rb.velocity.y); 
                    
                    if (rb.velocity.y > termVelocity)
                    {
                       // Debug.Log("TermVelocity: " + termVelocity); 

                        rb.velocity = new Vector3(rb.velocity.x, termVelocity, rb.velocity.z);
                    }
                    a.JumpData.Get(e).isJumping = false;
                    a.JumpData.Get(e).isFalling = true;
                }

            }
        }
    }
}