using System;
using DCFApixels.DragonECS;
using SOData;

namespace Components.Jump
{
    /// <summary>
    /// Данные системы прыжка
    /// </summary>
    [Serializable]
    public struct JumpComponent: IEcsComponent
    {
        public JumpSettingsSo settings; // настройки прыжка
        public float jumpPressedRemember; // время нажатия прыжка
        public float termVelocity; // конечная скорость прыжка
        public float termTime; // конечное время прыжка
        public float initJumpVelocity; // начальная скорость прыжка
        public float gravity; // сила гравитации
        public float cayoteTimeRemember; // время прыжка после падения
        
        public bool isJumping;
        public bool isFalling;
    }
    
    class JumpTemplate: ComponentTemplate<JumpComponent> { }
}