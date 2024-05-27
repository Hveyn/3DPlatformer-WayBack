using Components.Hover;
using SOData;
using UnityEngine;

namespace Services
{
    public class DebugDrawGizmosService: MonoBehaviour
    {
        private Transform _playerTransform;
        private HoverSettingsSo hoverSettings;

        public void SetParametrs(Transform player, HoverSettingsSo hover)
        {
            _playerTransform = player;
            hoverSettings = hover;
        }
        
        private void OnDrawGizmos()
        {
            if (_playerTransform != null)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawWireSphere(_playerTransform.position - _playerTransform.up * hoverSettings.maxDistance,
                    hoverSettings.castRadius);
            }
        }
    }
}