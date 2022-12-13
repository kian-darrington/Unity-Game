using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public GameObject Music;
    public GameObject MusicSetting;
    public GameObject SoundSetting;

    void Start()
    {

        CheckMusic();
        CheckSound();
    }

    public void CheckMusic()
    {
        
        if(PlayerPrefs.HasKey("MusicOn"))
        {
            if (PlayerPrefs.GetInt("MusicOn") > 0)
            {
                Music.SetActive(true);

                Toggle T = MusicSetting.GetComponent<Toggle>();
                T.isOn = true;

            }
            else
            {
                Music.SetActive(false);
                Toggle T = MusicSetting.GetComponent<Toggle>();
                T.isOn = false;
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
                //Sound.SetActive(true);
                SoundSetting.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                //Sound.SetActive(false);
                SoundSetting.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("SoundOn", 1);
            //Sound.SetActive(true);
        }
    }

    public void SetMusic()
    {
        if(PlayerPrefs.GetInt("MusicOn") > 0)
        {
            Music.SetActive(false);
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        else
        {
            Music.SetActive(true);
            PlayerPrefs.SetInt("MusicOn", 1);
        }
    }
   

}
