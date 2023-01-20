using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject Pause;

    // Start is called before the first frame update
    void Start()
    {
        Pause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (input.getKeyDown("Escape") && Pause.Active == false)
        {
            Pause.SetActive(true);
            time.timeScale = 0f;
        }
        else if (input.getKeyDown("Escape") && Pause.Active == true)
        {
            Pause.SetActive(false);
            time.timeScale = 1.0f;
        }
    }

    public void Leave()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Resume()
    {
        Pause.SetActive(false);
        time.timeScale = 1.0f;
    }

    public void Save()
    {
        DataPersistanceManager.instance.Save();
    }
}
