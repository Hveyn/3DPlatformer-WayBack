using DhafinFawwaz.AnimationUILib;
using DhafinFawwaz.AnimationUILib.Demo;
using UnityEngine;

namespace Services.Audio
{
public class AudioObserver : MonoBehaviour
{
    [SerializeField] AudioManager _audio;
    void OnEnable()
    {
        ButtonUI.s_onClick += ButtonOnClick;
        ButtonUI.s_onPointerEnter += ButtonEnter;
        ButtonUI.s_onPointerDown += ButtonDown;
        ButtonUI.s_onSelect += ButtonEnter;

        AnimationUI.OnPlaySoundByFile += _audio.PlaySound;
        AnimationUI.OnPlaySoundByIndex += _audio.PlaySound;
    }
    void OnDisable()
    {
        ButtonUI.s_onClick -= ButtonOnClick;
        ButtonUI.s_onPointerEnter -= ButtonEnter;
        ButtonUI.s_onPointerDown -= ButtonDown;
        ButtonUI.s_onSelect -= ButtonEnter;

        AnimationUI.OnPlaySoundByFile -= _audio.PlaySound;
        AnimationUI.OnPlaySoundByIndex -= _audio.PlaySound;
    }

    void ButtonEnter() => _audio.PlaySound(3);
    void ButtonOnClick() => _audio.PlaySound(0);
    void ButtonDown() => _audio.PlaySound(1);
}

}