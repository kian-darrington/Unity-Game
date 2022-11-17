using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;

    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found More than one Data Persistance Manager in the scene.");
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO - load any saved data from a file using the data handler
        //if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        //TODO - push the loaded data to all other scripts that I need it
    }
}
