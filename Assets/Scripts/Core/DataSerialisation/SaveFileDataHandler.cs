using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveFileDataHandler : MonoBehaviour
{
    private string _dataDirectory = "";
    private string _fileName = "";

    private bool _useEncryption = false;
    public readonly string _encryptionPass;

    public SaveFileDataHandler(string dataDirectory, string fileName, bool useEncryption) {
        _dataDirectory = dataDirectory;
        _fileName = fileName;
        _useEncryption = useEncryption;

        _encryptionPass = File.ReadAllText("Assets/Scripts/Core/DataSerialisation/encryptionPass.txt");
    }

    public GameData Load() {
        string fullDirPath = Path.Combine(_dataDirectory, _fileName);
        GameData loadedData = null;

        if (File.Exists(fullDirPath)) {
            try {
                string dataToLoad = "";
                using (FileStream s = new FileStream(fullDirPath, FileMode.Open)) { // Load save data from save file.
                    using (StreamReader r = new StreamReader(s)) {
                        dataToLoad = r.ReadToEnd();
                    }
                }

                if (_useEncryption) {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                return loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } catch (Exception e) {
                // TODO: Need to create a user front end for this error!
                Debug.LogError("Error occurred loading data from file: " + fullDirPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data) {
        // cause I do be on le linux, i use path.combine so it's cross-platform! :D
        string fullDirPath = Path.Combine(_dataDirectory, _fileName);
    
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullDirPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (_useEncryption) {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream s = new FileStream(fullDirPath, FileMode.Create)) { // Write save data to save file.
                using (StreamWriter w = new StreamWriter(s)) {
                    w.Write(dataToStore);
                }
            }
        } catch (Exception e) {
            // TODO: Need to create a user front end for this error!
            Debug.LogError("Error occurred saving data to file: " + fullDirPath + "\n" + e);
        }
    }

    public string EncryptDecrypt(string data) { // Simple as fuck XOR encryption
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++) {
            modifiedData += (char)(data[i] ^ _encryptionPass[i % _encryptionPass.Length]);
        }
        return modifiedData;
    }
}
