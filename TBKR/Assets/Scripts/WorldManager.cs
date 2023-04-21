using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WorldManager : MonoBehaviour
{
    Button[] buttons;
    List<bool> Availability;
    public static WorldManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one istance of World Manager found!");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        Availability = new List<bool> { true, true };
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = Availability[i];
        }
        StartCoroutine("DelaySettingStuffInactive");
    }

    IEnumerator DelaySettingStuffInactive()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }


    public void OpenTutorial()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenW1_1()
    {
        SceneManager.LoadScene("World 1-1");
    }
}
