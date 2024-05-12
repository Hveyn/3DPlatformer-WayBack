using UnityEngine;
using UnityEngine.InputSystem;

namespace Views
{
    public class PlayerInputSettingsView : MonoBehaviour
    {
        [Header("Input Action Asset")]
        public InputActionAsset playerConrols;

        [Header("Action Map Name References")] 
        public string actionMapName = "Player";

        [Header("Action Name References")] 
        public string move = "Move";
        public string look = "Look";
        public string jump = "Jump";
        public string sprint = "Sprint";

        [Header("Deadzone Values")] 
        public float leftStickDeadzoneValue = 0.2f;
    }
}
