using DCFApixels.DragonECS;
using Components;
using Components.Physics;
using UnityEngine;

namespace Client.Physics
{
    sealed class RelativeSpeedAlongDirectionSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<GetRelativeSpeedAlongDirection> GetSpeed;
        }

        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Rigidbody targetBody = a.GetSpeed.Get(e).targetBody;
                Rigidbody frameBody = a.GetSpeed.Get(e).frameBody;
                Vector3 direction = a.GetSpeed.Get(e).direction;
                
                Vector3 velocity = targetBody.velocity;
                Vector3 hitBodyVelocity = frameBody ? frameBody.velocity : default;
                float rayDirectionSpeed = Vector3.Dot(direction, velocity);
                float hitBodyRayDirectionSpeed = Vector3.Dot(direction, hitBodyVelocity);
                a.GetSpeed.Get(e).relativeSpeed = rayDirectionSpeed - hitBodyRayDirectionSpeed;
            }
        }
    }
}