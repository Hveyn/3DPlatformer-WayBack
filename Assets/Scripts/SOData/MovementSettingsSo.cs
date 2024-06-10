using UnityEngine;

namespace SOData
{
    /// <summary>
    /// Класс настройки скорости перемещения
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "ObjectSettings/MovementSettingsScriptableObject", order = 1)]
    public class MovementSettingsSo: ScriptableObject
    {
        public float maxSpeed = 3.0f;
        public float acceleration = 200f; 
        public float maxAccelForce = 150f;
        [Range(0,1)]
        public float airMultiplier = 0.8f; 
        public AnimationCurve maxAccelerationForceFromDot;
        public Vector3 forceScale;
    }
}