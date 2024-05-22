using System;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Components.Hover
{
    [Serializable]
    public struct HoverOnTag: IEcsTagComponent { }
    
    class HoverOnTagTemplate: TagComponentTemplate<HoverOnTag> { }
}