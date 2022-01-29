using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider soundsSlider;

    public AudioSource audioSource;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        soundsSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSoundsLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }

    public void OnMuteMusicButtonClick()
    {
        if (musicSlider.value != musicSlider.minValue)
        {
            SetMusicLevel(musicSlider.minValue);
            musicSlider.value = musicSlider.minValue;
        }
        else if (musicSlider.value == musicSlider.minValue)
        {
            SetMusicLevel(0.75f);
            musicSlider.value = 0.75f;
        }
        Debug.Log("Music muted");
    }

    public void OnMuteSoundsButtonClick()
    {
        if (soundsSlider.value != soundsSlider.minValue)
        {
            SetSoundsLevel(soundsSlider.minValue);
            soundsSlider.value = soundsSlider.minValue;
        }
        else if (soundsSlider.value == soundsSlider.minValue)
        {
            SetSoundsLevel(0.75f);
            soundsSlider.value = 0.75f;
        }
        Debug.Log("Sounds Muted");
    }
}
