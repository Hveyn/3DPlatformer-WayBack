using System;
using DCFApixels.DragonECS;
using SOData;
using UnityEngine;

namespace Components.Movement
{
    [Serializable]
    public struct MovementComponent: IEcsComponent
    {
        public MovementSettingsSo settings; // Настройки перемещения
        
        public Vector3 moveDirection; // направление
        public Vector3 goalVel; // целевая скорость
    }
    
    class MovementTemplate: ComponentTemplate<MovementComponent> { }
}