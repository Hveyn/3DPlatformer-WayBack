using System;
using DCFApixels.DragonECS;

namespace Components.ComponentTags
{
    [Serializable]
    public struct PrepareAnimationTag : IEcsTagComponent { }
    
    class PrepareAnimationTagTemplate: TagComponentTemplate<PrepareAnimationTag> { }

}