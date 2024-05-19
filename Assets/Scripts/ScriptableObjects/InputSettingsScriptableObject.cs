using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = nameof(InputSettingsScriptableObject), menuName = "ScriptableObjects/" + nameof(InputSettingsScriptableObject), order = 1)]
public class InputSettingsScriptableObject : ScriptableObject
{
    [Header("Input Action Asset")]
    public InputActionAsset playerControls;

    [Header("Action Map Name References")]
    public string actionMapName;

    [Header("Action Name References")]
    public string move;
    public string look;
    public string jump;
    public string sprint;

    [Header("Deadzone Values")]
    public float leftStickDeadzoneValue;
    //public float RightStickDeadzoneValue;
}
