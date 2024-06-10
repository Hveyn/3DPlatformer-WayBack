using System;
using DCFApixels.DragonECS;

namespace Components.ComponentTags
{
    [Serializable]
    public struct PrepareJumpComponentTag : IEcsTagComponent { }
    
    class PrepareJumpComponentTagTemplate: TagComponentTemplate<PrepareJumpComponentTag> { }
}