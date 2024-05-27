using UnityEngine;

namespace SOData
{
    [CreateAssetMenu(fileName = "Data", menuName = "ObjectSettings/JumpSettingsScriptableObject", order = 1)]
    public class JumpSettingsSo: ScriptableObject
    {
        public float jumpHeight = 1.0f;
        public float minJumpHeight = 0.2f;
        public float timeToApex = 0.5f;
        
        /// <summary>
        /// максимальное время после нажатия прыжка
        /// </summary>
        public float jumpPressedRememberTime = 0.2f;
        
        /// <summary>
        /// время после падения, когда игрок может выполнить прыжок
        /// </summary>
        public float cayoteTime = 0.2f;
        
    }
}