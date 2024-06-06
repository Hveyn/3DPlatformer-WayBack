using DCFApixels.DragonECS;
using Components.Camera;
using Components.Input;
using UnityEngine;


namespace Client {
    sealed class CinemachineRunSystem : IEcsRun
    {
        class Aspect : EcsAspectAuto
        {
            [Inc] public EcsPool<CinemamachineSettings> Camera;
            [Inc] public EcsPool<PlayerInputData> InputData;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run ()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                Transform camera = a.Camera.Get(e).camera;
                Transform orientation = a.Camera.Get(e).orientation;
                Transform player = a.Camera.Get(e).player;
                Transform playerObj = a.Camera.Get(e).playerObj;
                
                Vector3 viewDir = player.position - new Vector3(camera.position.x, player.position.y, camera.position.z);
                orientation.forward = viewDir.normalized;
        
                //rotate player object
                Vector3 inputDir = orientation.forward * a.InputData.Get(e).moveInput.y + orientation.right 
                                   * a.InputData.Get(e).moveInput.x;

                if (inputDir != Vector3.zero)
                    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, 
                                                    Time.deltaTime * a.Camera.Get(e).rotationSpeed);

            }
        }
    }
}