using Components.Camera;
using Components.Input;
using Components.PhysicsComponents;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Systems.CinemachineSystems {
    /// <summary>
    /// Система управления камерой
    /// </summary>
    sealed class CinemachineRunSystem : IEcsRun
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<CinemamachineSettings> Camera;
            [Inc] public EcsPool<PlayerInputData> InputData;
            [Inc] public EcsPool<OrientationObject> OrientationObjects;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Transform camera = a.Camera.Get(e).camera;
                Transform orientation = a.OrientationObjects.Get(e).orientation;
                Transform player = a.Camera.Get(e).player;
                Transform playerObj = a.Camera.Get(e).playerObj;
                
                Vector3 viewDir = player.position - new Vector3(camera.position.x, player.position.y, camera.position.z);
                orientation.forward = viewDir.normalized;
        
                //Вращение модели игрока
                Vector3 inputDir = orientation.forward * a.InputData.Get(e).moveInput.y + orientation.right 
                                   * a.InputData.Get(e).moveInput.x;

                if (inputDir != Vector3.zero)
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, 
                                                    Time.deltaTime * a.Camera.Get(e).rotationSpeed);

            }
        }
    }
}