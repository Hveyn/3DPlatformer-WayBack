using UnityEngine;

namespace Services.Audio
{
    /// <summary>
    /// Класс для описания звуковых файлов
    /// </summary>
    [System.Serializable] public class Sound
    {
        [Tooltip("Clip to play")]public AudioClip Clip;
        [Tooltip("Volume of the clip")]
        public float Volume = 1;
#if UNITY_EDITOR 
        [Tooltip("Just for naming, this isn't actually used anywhere")]public string ClipName;
#endif
    }
}