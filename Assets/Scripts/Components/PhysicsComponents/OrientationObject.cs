using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.PhysicsComponents
{
    [Serializable]
    public struct OrientationObject: IEcsComponent
    {
        public Transform orientation;
    }
    
    class OrientationObjectTemplate: ComponentTemplate<OrientationObject> { }
}