using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour, IDataPersistance
{
    public GameObject Music;
    public GameObject MusicSetting;
    public GameObject SoundSetting;
    public GameObject click;
    private int SoundOn = 1;
    private int MusicOn = 1;


    void Start()
    {
        CheckMusic();
        CheckSound();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           

        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }

        if (PlayerPrefs.GetInt("SoundOn") > 0)
        {
            click.SetActive(false);
            PlayerPrefs.SetInt("SoundOn", 0);
        }
        else
        {
            click.SetActive(true);
            PlayerPrefs.SetInt("SoundOn", 1);
        }
    }

    public void CheckMusic()
    {

        if (PlayerPrefs.HasKey("MusicOn"))
        {
            if (PlayerPrefs.GetInt("MusicOn") > 0)
            {
                Toggle T = MusicSetting.GetComponent<Toggle>();
                T.isOn = true;

                Music.SetActive(true);

                PlayerPrefs.SetInt("MusicOn", 1);

            }
            else
            {
                Toggle T = MusicSetting.GetComponent<Toggle>();
                T.isOn = false;
                Music.SetActive(false);
                PlayerPrefs.SetInt("MusicOn", 0);
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
        if (PlayerPrefs.HasKey("SoundOn"))
        {
            if (PlayerPrefs.GetInt("SoundOn") > 0)
            {
                Toggle S = SoundSetting.GetComponent<Toggle>();
                S.isOn = false;
                click.SetActive(true);
                PlayerPrefs.SetInt("SoundOn", 1);

            }
            else
            {
                Toggle S = SoundSetting.GetComponent<Toggle>();
                S.isOn = true;
                click.SetActive(false);
                PlayerPrefs.SetInt("SoundOn", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("SoundOn", 1);
            click.SetActive(true);
        }
    }

    public void SetMusic()
    {

        if (PlayerPrefs.GetInt("MusicOn") > 0)
        {
            Music.SetActive(false);
            PlayerPrefs.SetInt("MusicOn", 0);
            MusicOn = 0;
        }
        else
        {
            Music.SetActive(true);
            PlayerPrefs.SetInt("MusicOn", 1);
            MusicOn = 1;
        }
    }

    public void SetSound()
    {
        if (PlayerPrefs.GetInt("SoundOn") > 0)
        {
            click.SetActive(false);
            PlayerPrefs.SetInt("SoundOn", 0);
        }
        else
        {
            click.SetActive(true);
            PlayerPrefs.SetInt("SoundOn", 1);
        }
    }


    public void menuclick()
    {
        Debug.Log("Click");
        AudioSource buttonclick = click.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("SoundOn") > 0)
        {
            buttonclick.Play(0);
        }
    }
    
    public void LoadData(GameData data)
    {
        this.MusicOn = data.MusicOn;
        this.SoundOn = data.SoundOn;
    }

    public void SaveData(ref GameData data)
    {
        data.MusicOn = this.MusicOn;
        data.SoundOn = this.SoundOn;
    }
    
}
