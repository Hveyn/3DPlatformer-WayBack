using System;
using DCFApixels.DragonECS;

namespace Components.Jump
{
    [Serializable]
    public struct PreparePhysicsTag : IEcsTagComponent { }
    
    class PreparePhysicsTagTemplate: TagComponentTemplate<PreparePhysicsTag> { }

}