using System;
using DCFApixels.DragonECS;

namespace Components {
    [Serializable]
    struct ApplyHoverForceTag: IEcsTagComponent { }
    class ApplyHoverForceTagTemplate : TagComponentTemplate<ApplyHoverForceTag> { }
}