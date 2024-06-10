using Components;
using Components.Input;
using Components.Movement;
using Components.PhysicsComponents;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Systems.MovementSystems.MoveSystems
{
    /// <summary>
    /// Система обработки ввода игрока в перемещение персонажа
    /// </summary>
    sealed class MovementHandlerSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> InputData;
            [Inc] public EcsPool<MovementComponent> Move;
            [Inc] public EcsPool<OrientationObject> OrientationObjects;
            [Inc] public EcsTagPool<PlayerTag> Tag;
        }

        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Transform orientation = a.OrientationObjects.Get(e).orientation;
                
                var move = orientation.forward * a.InputData.Get(e).moveInput.y +
                           orientation.right * a.InputData.Get(e).moveInput.x;
                
                if (move.magnitude > 1.0f)
                    move.Normalize();
                
                a.Move.Get(e).moveDirection = move;
            }
        }
    }
}