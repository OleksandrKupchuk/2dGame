using System;
using System.IO;
using UnityEngine;

public class SaveLoadJsonSystem : ISaveLoadSystem {
    public void Save(string fileName, object data) {
        string _filePath = Application.persistentDataPath + $"/{fileName}.json";

        if (File.Exists(_filePath)) {
            File.Delete(_filePath);
        }

        string _json = JsonUtility.ToJson(data, true);
        File.AppendAllText(_filePath, _json);
    }

    public T Load<T>(string fileName) {
        string _filePath = Application.persistentDataPath + $"/{fileName}.json";

        try {
            string _json = File.ReadAllText(_filePath);
            T _data = JsonUtility.FromJson<T>(_json);
            return _data;

        }
        catch (FileNotFoundException) {
            throw new Exception($"File '{fileName}' not exist, please check file path '{_filePath}'");
        }
    }
}
