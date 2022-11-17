using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{

    public GameObject maincam;
    public GameObject settingcam;
    public GameObject creditscam;
    public GameObject loadcam;
    public GameObject SettingsCan;
    public GameObject MainCan;
    public GameObject CreditsCan;
    public GameObject LoadCan;


    void Start()
    {
        settingcam.SetActive(false);
        creditscam.SetActive(false);
        loadcam.SetActive(false);

        SettingsCan.SetActive(false);
        LoadCan.SetActive(false);
        CreditsCan.SetActive(false);
    }

    public void Continue()
    {
        
    }

    public void NewGame()
    {

    }

    public void Credits()
    {
        maincam.SetActive(false);
        settingcam.SetActive(false);
        creditscam.SetActive(true);
        loadcam.SetActive(false);

        MainCan.SetActive(false);
        SettingsCan.SetActive(false);
        LoadCan.SetActive(false);
        CreditsCan.SetActive(true);
    }

    public void Settings()
    {
        settingcam.SetActive(true);
        maincam.SetActive(false);
        creditscam.SetActive(false);
        loadcam.SetActive(false);

        MainCan.SetActive(false);
        SettingsCan.SetActive(true);
        LoadCan.SetActive(false);
        CreditsCan.SetActive(false);
    }

    public void Back()
    {
        maincam.SetActive(true);
        settingcam.SetActive(false);
        creditscam.SetActive(false);
        loadcam.SetActive(false);

        MainCan.SetActive(true);
        SettingsCan.SetActive(false);
        LoadCan.SetActive(false);
        CreditsCan.SetActive(false);
    }
}
