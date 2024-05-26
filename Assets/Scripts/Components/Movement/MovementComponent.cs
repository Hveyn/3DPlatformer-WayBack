using System;
using DCFApixels.DragonECS;
using SOData;
using UnityEngine;

namespace Components.Movement
{
    [Serializable]
    public struct MovementComponent: IEcsComponent
    {
        public MovementSettingsSo settings;
        
        public Transform orientationObject;
        public Vector3 moveDirection;
        public Vector3 goalVel;
    }
    
    class MovementTemplate: ComponentTemplate<MovementComponent> { }
}