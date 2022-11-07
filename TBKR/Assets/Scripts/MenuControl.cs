using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{

    public GameObject maincam;
    public GameObject settingcam;
    public GameObject creditscam;
    public GameObject loadcam;

    public void Continue()
    {

    }

    public void Load()
    {

    }

    public void NewGame()
    {

    }

    public void Credits()
    {

    }

    public void Settings()
    {
        settingcam.SetActive(true);
        maincam.SetActive(false);
    }

    public void Back()
    {
        maincam.SetActive(true);
        settingcam.SetActive(false);
        creditscam.SetActive(false);
        loadcam.SetActive(false);
    }
}
