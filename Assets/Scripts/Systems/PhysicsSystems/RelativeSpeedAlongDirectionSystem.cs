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
                Vector3 velocity = a.GetSpeed.Get(e).frameBody.velocity;
                Vector3 hitBodyVelocity = a.GetSpeed.Get(e).frameBody ? a.GetSpeed.Get(e).frameBody.velocity : default;
                float rayDirectionSpeed = Vector3.Dot(a.GetSpeed.Get(e).direction, velocity);
                float hitBodyRayDirectionSpeed = Vector3.Dot(a.GetSpeed.Get(e).direction, hitBodyVelocity);
                a.GetSpeed.Get(e).result = rayDirectionSpeed - hitBodyRayDirectionSpeed;
            }
        }
    }
}