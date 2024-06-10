using System;
using DCFApixels.DragonECS;

namespace Components.ComponentTags
{
    [Serializable]
    public struct PreparePhysicsTag : IEcsTagComponent { }
    
    class PreparePhysicsTagTemplate: TagComponentTemplate<PreparePhysicsTag> { }

}