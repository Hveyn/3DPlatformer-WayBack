using System;
using DCFApixels.DragonECS;

namespace Components.Jump
{
    [Serializable]
    public struct PrepareAnimationTag : IEcsTagComponent { }
    
    class PrepareAnimationTagTemplate: TagComponentTemplate<PrepareAnimationTag> { }

}