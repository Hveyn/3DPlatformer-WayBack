using Unity.Collections;
using UnityEngine;

namespace SOData
{
    [CreateAssetMenu(fileName = "Data", menuName = "ObjectSettings/JumpSettingsScriptableObject", order = 1)]
    public class JumpSettingsSo: ScriptableObject
    {
        public float jumpHeight = 1.0f;
        public float minJumpHeight = 0.2f;
        public float timeToApex = 0.5f;
        public float jumpPressedRememberTime = 0.2f;
    }
}