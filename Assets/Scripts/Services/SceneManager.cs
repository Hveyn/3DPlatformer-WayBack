using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
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