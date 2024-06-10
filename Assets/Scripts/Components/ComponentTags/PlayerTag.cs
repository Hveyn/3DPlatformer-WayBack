using System;
using DCFApixels.DragonECS;

namespace Components
{
    [Serializable]
    struct PlayerTag: IEcsTagComponent { }
    class PlayerTagTemplate : TagComponentTemplate<PlayerTag> { }
}