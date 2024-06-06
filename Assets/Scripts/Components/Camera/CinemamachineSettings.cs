using System;
using Cinemachine;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Camera
{
    [Serializable]
    public struct CinemamachineSettings: IEcsComponent
    {
        public CinemachineFreeLook cinemachineFreeLook;
        public Transform camera;
        public bool invertYAxis;
        public float rotationSpeed;
        
        //отделить в новый компонент
        public Transform orientation;
        
        public Transform player;
        public Transform playerObj;
    }
    
    class CinemamachineSettingsTemplate: ComponentTemplate<CinemamachineSettings> { }

}