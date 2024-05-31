using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Services.Audio
{
public class AudioManager : MonoBehaviour
{
    [Header("Audio settings")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private AudioClip defaultSound;
    
    [Header("UI")]
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleSound;
    [SerializeField] private Slider sliderMusicValue;
    [SerializeField] private Slider sliderSoundValue;

    public Sound[] SFX;

    private void Start()
    {
        toggleMusic.isOn = PlayerPrefs.GetInt("MusicOn",1) == 1;
        toggleSound.isOn = PlayerPrefs.GetInt("SoundOn",1) == 1;
        sliderMusicValue.value = PlayerPrefs.GetFloat("MusicVolume",1);
        sliderSoundValue.value = PlayerPrefs.GetFloat("SoundVolume",1);
    }

    public void PlaySound(AudioClip audioClip, float volume)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        soundSource.PlayOneShot(audioClip, volume);
    }
    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip == null)
        {
            PlayDefaultSound();
            return;
        }
        soundSource.PlayOneShot(audioClip);
    }
    void PlayDefaultSound()
    {
        if(defaultSound != null)
         soundSource.PlayOneShot(defaultSound);
    }
    
    public void PlaySound(int index)
    {
        if(index > SFX.Length-1)
        {
            Debug.LogWarning("Please assign the clip at index " + index.ToString());
        }
        PlaySound(SFX[index].Clip, SFX[index].Volume);
    }
    
    public void ToggleMusic(bool isOn)
    {
        if (isOn)
            mixer.audioMixer.SetFloat("MusicVolume", 0);
        else
            mixer.audioMixer.SetFloat("MusicVolume", -80);
        
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
    }

    public void ToggleSound(bool isOn)
    {
        if (isOn)
            mixer.audioMixer.SetFloat("SoundVolume", 0);
        else
            mixer.audioMixer.SetFloat("SoundVolume", -80);
        
        PlayerPrefs.SetInt("SoundOn", isOn ? 1 : 0);
    }
    
    public void ChangeMasterVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80,0,volume));
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    
    public void ChangeSoundsVolume(float volume)
    {
        mixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80,0,volume));
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
    
    public void ChangeMusicVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80,0,volume));
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
}

}