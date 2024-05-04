using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class DataPersistenceManager : MonoBehaviour {
    public static DataPersistenceManager instance { get; private set; }

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistences;
    private SaveFileDataHandler _dataFileHandler;

    [Header("File Storage Config")]
    [SerializeField] private string _fileName;
    [SerializeField] public bool encryptSaveData;

    public void Awake() {
        if (instance != null) {
            Debug.LogError("More than one Data Persistence Manager found in current scene.");
        }
        instance = this;
    }

    public void Start() {
        _dataFileHandler = new SaveFileDataHandler(Application.persistentDataPath, _fileName, encryptSaveData);
        _dataPersistences = FindAllDataPersistenceObjects();
        LoadGame();    
    }

    public void NewGame() {
        _gameData = new GameData();
    }

    public void LoadGame() {
        _gameData = _dataFileHandler.Load();

        if (_gameData == null) {
            // TODO: Could benefit with a user front end telling this to the player.
            Debug.LogError("No data was found. Initialising Data");
            NewGame();
        }
        
        // push all loaded data to scripts that need it
        foreach (IDataPersistence dataPersistence in _dataPersistences) {
            dataPersistence.LoadData(_gameData);
        }
    }

    public void SaveGame() {
        // pass data to scripts so they can update it.
        foreach (IDataPersistence dataPersistence in _dataPersistences) {
            dataPersistence.SaveData(ref _gameData);
        }
        _dataFileHandler.Save(_gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> _dataPersistences = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(_dataPersistences);
    }

    private void OnApplicationQuit() {
        SaveGame();    
    }
}
