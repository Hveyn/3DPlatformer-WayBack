using System;
using DhafinFawwaz.AnimationUILib;
using Mono.InputControl;
using UnityEngine;

public class HudGameConrol : MonoBehaviour
{
    [SerializeField] private PlayerInputHandlerService inputHandlerService;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AnimationUI animationOpen;

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
    }
}
