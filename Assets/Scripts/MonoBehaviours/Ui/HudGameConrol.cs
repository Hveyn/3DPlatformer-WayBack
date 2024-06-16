using DhafinFawwaz.AnimationUILib;
using Mono.InputControl;
using Services;
using Services.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Класс управление HUD игрока
/// </summary>
public class HudGameConrol : MonoBehaviour
{
    [Header("Ui elements")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject defaultButtonPauseMenu;
    
    [Header("AnimationsUI")]
    [SerializeField] private AnimationUI animationSettingsClose;
    [SerializeField] private AnimationUI animationOpen;
    
    [Header("Bonus elements")]
    [SerializeField] private TextMeshProUGUI textCountBonus;
    [SerializeField] private BonusManager bonusManager;
    
    [Header("Services")]
    [SerializeField] private PlayerInputHandlerService inputHandlerService;
    [SerializeField] private MenuNavigation menuNavigation;
    
    private bool isPause;
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
                if (!isPause)
                {
                    Pause();
                }
                else if (isPause && !settingMenu.activeSelf)
                {
                    UnPause();
                }
                else
                {
                    CancelSettings();
                }
            }
            
        }

        textCountBonus.text = bonusManager.CountBonus.ToString();
    }

    private void CancelSettings()
    {
        if (animationOpen != null) animationSettingsClose.PlayReversed();
        menuNavigation.SetSelectedUI(defaultButtonPauseMenu);
    }

    #region Pause/Unpause Functions
    private void Pause()
    {
        isPause = true;
        pausePanel.SetActive(true);
        if (animationOpen != null) animationOpen.Play();
    }
    
    private void UnPause()
    {
        isPause = false;
        if (animationOpen != null) animationOpen.PlayReversed();
        pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    #endregion
}
