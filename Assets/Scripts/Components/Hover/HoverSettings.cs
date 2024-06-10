using System;
using DCFApixels.DragonECS;
using SOData;

namespace Components.Hover
{
    /// <summary>
    /// Хранение настроект парения над поверхностью
    /// </summary>
    [Serializable]
    public struct HoverSettings: IEcsComponent
    {
        public HoverSettingsSo settings;
    }
    
    class HoverSettingsTemplate: ComponentTemplate<HoverSettings> { }
}