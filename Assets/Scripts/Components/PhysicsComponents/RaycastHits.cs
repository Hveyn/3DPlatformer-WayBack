using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.PhysicsComponents
{
    [Serializable]
    public struct RaycastHits: IEcsComponent
    {
        public RaycastHit[] Hits;
    }
    
    class HoverSettingsTemplate: ComponentTemplate<RaycastHits> { }
}