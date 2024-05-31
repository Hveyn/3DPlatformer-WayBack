using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Services
{
    public class MenuNavigation: MonoBehaviour
    {
        [SerializeField] private GameObject defaultSelectedObject;
        
        public void SetSelectedUI(GameObject uiElement)
        {
            EventSystem.current.SetSelectedGameObject(uiElement);
            defaultSelectedObject = uiElement;
        }

        private void Update()
        {
            if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame &&
                EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(defaultSelectedObject);
            }
        }
    }
}