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
        /// JumpPressedRememberTime время нажатия прыжка
        /// </summary>
        public float coyoteTime = 0.2f;
    }
}