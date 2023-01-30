using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{

    public GameObject maincam;
    public GameObject settingcam;
    public GameObject creditscam;
    public GameObject SettingsCan;
    public GameObject MainCan;
    public GameObject CreditsCan;
    public GameObject background;

    void Start()
    {
        settingcam.SetActive(false);
        creditscam.SetActive(false);

        SettingsCan.SetActive(false);
        CreditsCan.SetActive(false);

        Debug.Log("PlayOption = " + PlayerPrefs.GetInt("PlayOption"));
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("PlayOption", 2);
        Debug.Log("Play option set to 2, in fact = " + PlayerPrefs.GetInt("PlayOption"));
        SceneManager.LoadScene("SampleScene");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("PlayOption", 1);
        Debug.Log("Play option set to 1, in fact = " + PlayerPrefs.GetInt("PlayOption"));
        SceneManager.LoadScene("SampleScene");
    }

    public void Credits()
    {
        maincam.SetActive(false);
        settingcam.SetActive(false);
        creditscam.SetActive(true);

        MainCan.SetActive(false);
        SettingsCan.SetActive(false);
        CreditsCan.SetActive(true);
    }

    public void Settings()
    {
        settingcam.SetActive(true);
        maincam.SetActive(false);
        creditscam.SetActive(false);

        MainCan.SetActive(false);
        SettingsCan.SetActive(true);
        CreditsCan.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Back()
    {
        maincam.SetActive(true);
        settingcam.SetActive(false);
        creditscam.SetActive(false);

        MainCan.SetActive(true);
        SettingsCan.SetActive(false);
        CreditsCan.SetActive(false);
    }
}
