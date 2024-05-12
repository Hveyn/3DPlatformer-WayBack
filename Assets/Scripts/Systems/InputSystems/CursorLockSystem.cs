using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class CursorLockSystem : IEcsInitSystem {
        public void Init (IEcsSystems systems) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}