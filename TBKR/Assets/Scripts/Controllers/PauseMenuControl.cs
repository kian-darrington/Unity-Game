
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject Pause;
    public GameObject click;

    // Start is called before the first frame update
    void Start()
    {
        Pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Pause.activeSelf == false)
        {
            Pause.SetActive(true);
            if (PlayerPrefs.GetInt("SoundOn") > 0)
            {
                click.GetComponent<AudioSource>().Play(0);
            }
            Time.timeScale = 0f;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Pause.activeSelf == true)
        {
            Pause.SetActive(false);
            Time.timeScale = 1.0f;
            if (PlayerPrefs.GetInt("SoundOn") > 0)
            {
                click.GetComponent<AudioSource>().Play(0);
            }
        }
    }

    public void Leave()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Resume()
    {
        Pause.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Save()
    {
        DataPersistanceManager.instance.SaveGame();
    }

}
