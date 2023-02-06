using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistance> dataPersisanceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found More than one Data Persistance Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersisanceObjects = FindAllDataPersistanceObjects();
        if (PlayerPrefs.GetInt("PlayOption") == 2 && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Menu"))
        {
            Debug.Log("Load");
            LoadGame();
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        //if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        //TODO - push the loaded data to all other scripts that need it
        foreach (IDataPersistance dataPersistanceObj in dataPersisanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        Debug.Log("Called Save Game");
        //TODO - pass the data to the other scripts so they can update it
        foreach (IDataPersistance dataPersistanceObj in dataPersisanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        Debug.Log("Saved Music setting = " + gameData.MusicOn);
        //save that data to a file using the datahandler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
