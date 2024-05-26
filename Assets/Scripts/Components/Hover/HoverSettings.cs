using System;
using DCFApixels.DragonECS;
using SOData;
using UnityEngine;

namespace Components.Hover
{
    [Serializable]
    public struct HoverSettings: IEcsComponent
    {
        public HoverSettingsSO settings;
    }
    
    class HoverSettingsTemplate: ComponentTemplate<HoverSettings> { }
}