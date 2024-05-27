using Cinemachine;
using Mono.InputControl;
using UnityEngine;

namespace Mono.CameraControl
{
    public class ThirdPersCamera : MonoBehaviour
    {
        [Header("Cinemamachine settings")] 
        [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
        [SerializeField] private bool invertYAxis;
        [SerializeField] private float rotationSpeed;
    
        [Header("Transforms")] 
        [SerializeField] private Transform orientation;
        [SerializeField] private Transform player;
        [SerializeField] private Transform playerObj;
    
        private PlayerInputHandlerService inputHandlerService;

        private void Start()
        {
            inputHandlerService = PlayerInputHandlerService.Instance;
        }

        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            //rotate orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;
        
            //rotate player object
            Vector3 inputDir = orientation.forward * inputHandlerService.MoveInput.y + orientation.right * inputHandlerService.MoveInput.x;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
