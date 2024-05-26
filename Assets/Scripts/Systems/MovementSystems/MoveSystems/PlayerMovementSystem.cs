using System.Runtime.InteropServices;
using Components.Input;
using Components.Movement;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client.MovementSystems.MoveSystems
{
    sealed class PlayerMovementSystem: IEcsFixedRunProcess
    {
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<PlayerInputData> InputData;
            [Inc] public EcsPool<MovementComponent> Move;
        }

        [EcsInject] private EcsDefaultWorld _world;
        
        public void FixedRun()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Transform orientation = a.Move.Get(e).orientationObject;
                
                var move = orientation.forward * a.InputData.Get(e).moveInput.y +
                           orientation.right * a.InputData.Get(e).moveInput.x;
                
                if (move.magnitude > 1.0f)
                    move.Normalize();
                
                a.Move.Get(e).moveDirection = move;
            }
        }
    }
}