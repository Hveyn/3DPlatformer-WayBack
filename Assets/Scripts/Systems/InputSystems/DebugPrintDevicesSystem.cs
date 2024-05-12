using DCFApixels.DragonECS;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client {
    sealed class DebugPrintDevices : IEcsInit {        
        public void Init () {
            foreach (var device in InputSystem.devices)
            {
                if (device.enabled)
                {
                    UnityDebugService.Activate();
                    EcsDebug.Print("Active Device: " + device.name);
                }
            }
        }
    }
}