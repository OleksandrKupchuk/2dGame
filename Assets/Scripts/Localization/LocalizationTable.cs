using System.IO;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Tables;

public class LocalizationTable {
    private string _mainDirectory = "Assets/Localization/Tables/";
    private string _directory;
    private string _tableName;
    private string _mainLocaleIdentifier = "en-US";
    private string _fileFormat = ".asset";

    public LocalizationTable(string subdirectory, string tableName) {
        _tableName = tableName;
        TryCreateDirectory(subdirectory);
        TryCreateTable();
    }

    private void TryCreateDirectory(string subdirectory) {
        if (string.IsNullOrEmpty(subdirectory)) {
            Debug.LogError("Subdirectory cant be null or empty");
        }

        _directory = _mainDirectory + subdirectory;

        if (Directory.Exists(_directory)) {
            Debug.LogWarning($"Directory is already exist {_directory}");
        }
        else {
            Directory.CreateDirectory(_directory);
            Debug.Log($"Directory created {_directory}");
        }
    }

    private void TryCreateTable() {
        if (string.IsNullOrEmpty(_tableName)) {
            Debug.LogError("Table name cant be null or empty");
        }

        string _filePath = _directory + "/" + _tableName + _fileFormat;

        if (File.Exists(_filePath)) {
            Debug.LogWarning($"Table name '{_tableName}' is exist in folder '{_directory}', you can not have two tables with same name");
        }
        else {
            LocalizationEditorSettings.CreateStringTableCollection(_tableName, _directory);
        }
    }

    public void AddEntry(string key, string value) {
        StringTableCollection _tableCollection = LocalizationEditorSettings.GetStringTableCollection(_tableName);
        StringTable _table = _tableCollection.GetTable(_mainLocaleIdentifier) as StringTable;

        if (_table == null) {
            Debug.LogError($"Table '{_tableName}' does not contain locale identifier '{_mainLocaleIdentifier}', try add this locale, or check _table name");
        }
        else {
            StringTableEntry _detailedLocalizationTable = _table.GetEntry(key);

            if (_detailedLocalizationTable == null) {
                _table.AddEntry(key, value);
                Debug.Log($"Added entry, key = {key}, value = {value}");
            }
            else {
                _table.RemoveEntry(key);
                _table.AddEntry(key, value);
                Debug.LogWarning($"Entry was overwritten, key = {key}, value = {value}");
            }
        }
    }

    public void RemoveEntry(string key) {
        StringTableCollection tableCollection = LocalizationEditorSettings.GetStringTableCollection(_tableName);
        StringTable _table = tableCollection.GetTable(_mainLocaleIdentifier) as StringTable;

        if (_table == null) {
            Debug.LogError($"Table '{_tableName}' does not contain locale identifier '{_mainLocaleIdentifier}', try add this locale, or check _table name");
        }
        else {
            _table.RemoveEntry(key);
            Debug.LogWarning($"Entry was deleted, key = {key}");
        }
    }
}
