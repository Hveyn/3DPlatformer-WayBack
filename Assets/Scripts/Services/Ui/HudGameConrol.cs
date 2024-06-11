using DhafinFawwaz.AnimationUILib;
using Mono.InputControl;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс управление HUD игрока
/// </summary>
public class HudGameConrol : MonoBehaviour
{
    [SerializeField] private PlayerInputHandlerService inputHandlerService;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AnimationUI animationOpen;

    [Header("Bonus elements")]
    [SerializeField] private TextMeshProUGUI textCountBonus;
    [SerializeField] private BonusManager bonusManager;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (inputHandlerService != null && pausePanel != null)
        {
            if (inputHandlerService.PauseTriggered)
            {
                if (!pausePanel.activeSelf)
                {
                    pausePanel.SetActive(true);
                    if (animationOpen != null) animationOpen.Play();
                }
            }

        }

        textCountBonus.text = bonusManager.CountBonus.ToString();
    }
}
