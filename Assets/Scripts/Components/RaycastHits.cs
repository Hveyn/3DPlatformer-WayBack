using System;
using DCFApixels.DragonECS;
using SOData;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct RaycastHits: IEcsComponent
    {
        public RaycastHit[] Hits;
    }
    
    class HoverSettingsTemplate: ComponentTemplate<RaycastHits> { }
}