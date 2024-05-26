using System.Linq;
using Components.Input;
using Components.Movement;
using Components.Physics;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.Physics
{
    public class JumpSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<JumpComponent> JumpData;
            [Inc] public EcsPool<GroundCastResult> GroundCast;
            [Inc] public EcsPool<UnityComponent<Rigidbody>> Rb;
        }
        
        [EcsInject] private EcsDefaultWorld _world;

        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                if (a.JumpData.Get(e).jumpPressedRemember > 0 &&
                    a.GroundCast.Get(e).resultCast)
                {
                    Rigidbody rb = a.Rb.Get(e).obj;
                    //Debug.Log($"JumpVelocity: {a.JumpData.Get(e).initJumpVelocity}");
                    rb.velocity = new Vector3(rb.velocity.x, a.JumpData.Get(e).initJumpVelocity, rb.velocity.z);
                }
            }
        }
    }
}