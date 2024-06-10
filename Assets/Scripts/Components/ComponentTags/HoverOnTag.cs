using System;
using DCFApixels.DragonECS;

namespace Components.Hover
{
    [Serializable]
    public struct HoverOnTag: IEcsTagComponent { }
    
    class HoverOnTagTemplate: TagComponentTemplate<HoverOnTag> { }
}