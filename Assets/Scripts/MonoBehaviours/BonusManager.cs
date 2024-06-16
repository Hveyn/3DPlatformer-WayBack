using UnityEngine;
using UnityEngine.VFX;

namespace Services
{
    /// <summary>
    /// Класс подсчёта бонусов
    /// </summary>
    public class BonusManager : MonoBehaviour
    {
        [SerializeField] private int countBonus;
        [SerializeField] private GameObject pickUpEffect;
    
        public int CountBonus => countBonus;

        private void Start()
        {
            countBonus = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bonus"))
            {
                countBonus++;
                if (pickUpEffect != null)
                {
                    GameObject copy = Instantiate(pickUpEffect, gameObject.transform);
                    Destroy(copy, 1f);
                }
                Destroy(other.gameObject);
            }
        }
    }
}
