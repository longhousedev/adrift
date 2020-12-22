using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Toggle difficulty;
    public Slider volumeSlider;
    public Toggle music;
    public bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        if (firstTime)
        {
            PlayerPrefs.SetInt("Difficulty", 1);
            PlayerPrefs.SetFloat("Volume", 100);
            PlayerPrefs.SetInt("Music", 1);
        }
        
        bool isMusic;
        bool isHard;
        if (PlayerPrefs.GetInt("Music") == 1) isMusic = true;
        else isMusic = false;
        if (PlayerPrefs.GetInt("Difficulty") == 1) isHard = true;
        else isHard = false;

        difficulty.isOn = isHard;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        music.isOn = isMusic;
    }
    
    public void MusicToggle()
    {
        PlayerPrefs.SetInt("Music", music.isOn ? 1 : 0);
    }

    public void VolumeSlider()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void HardMode()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty.isOn ? 1 : 0);
        
    }

    public void CloseButton()
    {
        gameObject.SetActive(false);
    }
    
    public void SaveButton()
    {
        PlayerPrefs.Save();
    }
    
    public void DefaultButton()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        PlayerPrefs.SetFloat("Volume", 100);
        PlayerPrefs.SetInt("Music", 1);
        volumeSlider.value = 100f;
        difficulty.isOn = true;
        music.isOn = true;
    }
}
