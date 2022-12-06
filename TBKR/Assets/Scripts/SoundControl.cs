using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public GameObject Music;
    public GameObject Sound;
    GameObject MusicSetting;
    GameObject SoundSetting;

    void Start()
    {
        CheckMusic();
        CheckSound();
        MusicSetting = GameObject.Find("Music Toggle");
        SoundSetting = GameObject.Find("Sound Toggle");
    }

    public void CheckMusic()
    {
        if(PlayerPrefs.HasKey("MusicOn"))
        {
            if (PlayerPrefs.GetInt("MusicOn") > 0)
            {
                Music.SetActive(true);
                MusicSetting.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                Music.SetActive(false);
                MusicSetting.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            Music.SetActive(true);
        }
    }

    public void CheckSound()
    {
        if(PlayerPrefs.HasKey("SoundOn"))
        {
            if (PlayerPrefs.GetInt("SoundOn") > 0)
            {
                Sound.SetActive(true);
                SoundSetting.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                Sound.SetActive(false);
                SoundSetting.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("SoundOn", 1);
            Sound.SetActive(true);
        }
    }
   

}
