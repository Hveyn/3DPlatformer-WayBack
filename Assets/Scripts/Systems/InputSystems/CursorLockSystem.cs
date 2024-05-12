using DCFApixels.DragonECS;
using UnityEngine;

namespace Client {
    sealed class CursorLockSystem : IEcsInit {
        public void Init()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}