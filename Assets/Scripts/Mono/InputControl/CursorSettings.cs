using UnityEngine;

namespace Mono.InputControl
{
    public class CursorSettings : MonoBehaviour
    {
    
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
        }
    }
}
