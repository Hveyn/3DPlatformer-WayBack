using UnityEngine;

namespace SOData
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HoverSettingsScriptableObject", order = 1)]
    public class HoverSettingsSO: ScriptableObject
    {
        public float dampFactor = 1;
        public float dampFrequency = 15;
        public float hoverHeight = 1.5f;
        public float maxDistance = 2;
        public float castRadius = .5f; 
        public LayerMask detectionLayers;
    }
}