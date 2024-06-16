using UnityEngine;
using UnityEngine.VFX;

namespace Services
{
    public class AnimationVFXService : MonoBehaviour
    {
        [SerializeField] private VisualEffect smokeVFX;

        public void PlayDust()
        {
            if(smokeVFX != null)
                smokeVFX.Play();
        }

        public void StopDust()
        {
            if(smokeVFX != null)
                smokeVFX.Stop();
        }
    }
}
