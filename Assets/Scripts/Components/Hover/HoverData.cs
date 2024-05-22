using System;
using DCFApixels.DragonECS;
using SOData;
using UnityEngine;

namespace Components.Hover
{
    [Serializable]
    public struct HoverData: IEcsComponent
    {
        public HoverSettingsSO settings;
        public float springDelta;
        public float springStrength;
        public float dampStrength;
        public float springSpeed;
        public Vector3 springForce;
    }
    
    class HoverSettingsTemplate: ComponentTemplate<HoverData> { }
}