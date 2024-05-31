using System;
using DCFApixels.DragonECS;

namespace Components.Input
{
    [Serializable]
    public struct CursorLockOn: IEcsComponent
    {
        public bool isLock;
    }
    
    class CursorLockOnTemplate: ComponentTemplate<CursorLockOn> { }
}