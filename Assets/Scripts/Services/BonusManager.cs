using UnityEngine;

namespace Services
{
    /// <summary>
    /// Класс подсчёта бонусов
    /// </summary>
    public class BonusManager : MonoBehaviour
    {
        [SerializeField] private int countBonus;
    
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
                Destroy(other.gameObject);
            }
        }
    }
}
