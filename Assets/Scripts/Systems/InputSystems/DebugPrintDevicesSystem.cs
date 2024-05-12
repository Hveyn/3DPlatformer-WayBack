using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client {
    sealed class DebugPrintDevices : IEcsInitSystem {        
        public void Init (IEcsSystems systems) {
            foreach (var device in InputSystem.devices)
            {
                if (device.enabled)
                {
                    Debug.Log("Active Device: " + device.name);
                }
            }
        }
    }
}