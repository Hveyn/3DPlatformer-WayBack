using System;
using DCFApixels.DragonECS;

namespace Components.Jump
{
    [Serializable]
    public struct PrepareJumpComponentTag : IEcsTagComponent { }
    
    class PrepareJumpComponentTagTemplate: TagComponentTemplate<PrepareJumpComponentTag> { }
}