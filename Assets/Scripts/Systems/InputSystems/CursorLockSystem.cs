using Components.Input;
using DCFApixels.DragonECS;
using UnityEngine;

namespace Client {
    sealed class CursorLockSystem : IEcsRun {
        
        class Aspect: EcsAspectAuto
        {
            [Inc] public EcsPool<CursorLockOn> CursorLock;
        }
        
        [EcsInject] private EcsDefaultWorld _world;
        
        public void Run()
        {
            foreach (var e in _world.Where(out Aspect a))
            {
                if (a.CursorLock.Get(e).isLock && Cursor.visible)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Confined;
                }
                else if(!a.CursorLock.Get(e).isLock)
                {
                    Cursor.visible = true;
                }
            }
        }
    }
}