using DCFApixels.DragonECS;
using System;

namespace Components {
    [Serializable]
    struct ApplyInputSettingsTag: IEcsTagComponent { }
    
    class ApplyInputSettingsTagTemplate : TagComponentTemplate<ApplyInputSettingsTag> { }
}