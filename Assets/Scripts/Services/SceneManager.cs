using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    /// <summary>
    /// Управление сценами в игре
    /// </summary>
    public class SceneManager: MonoBehaviour
    {
        
        public void SetSelectedUI(GameObject uiElement)
        {
            EventSystem.current.SetSelectedGameObject(uiElement);
        }
        
        public void Close()
        {
            Application.Quit();
        }
    }
}