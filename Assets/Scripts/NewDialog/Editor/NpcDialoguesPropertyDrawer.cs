using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NpcDialogues))]
public class NpcDialoguesPropertyDrawer : Editor {
    private SerializedProperty _npcNameProperty;
    private SerializedProperty _dialoguesDataProperty;
    private string _mainDirectory;
    private LocalizationTable _localizationTable;

    private void OnEnable() {
        _npcNameProperty = serializedObject.FindProperty("_npcName");
        _dialoguesDataProperty = serializedObject.FindProperty("_dialoguesData");
        _mainDirectory = $"Npc/NpcDialogues/{_npcNameProperty.stringValue}/";
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        DrawDefaultInspector();

        CheckValidationStringFieldAndDrawHelpBox(_npcNameProperty);

        if (GUILayout.Button("Create Localization Tables")) {
            DeleteLocalizationFolder(_mainDirectory);

            int _count = _dialoguesDataProperty.arraySize;

            for (int i = 0; i < _count; i++) {
                SerializedProperty _property = _dialoguesDataProperty.GetArrayElementAtIndex(i);
                CreateLocalizationTable(i);
                AddEntriesForProperty(_property);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CheckValidationStringFieldAndDrawHelpBox(SerializedProperty property) {
        if (string.IsNullOrEmpty(property.stringValue)) {
            EditorGUILayout.HelpBox($"Field '{property.displayName}' can not be null or empty", MessageType.Error);
            return;
        }
    }

    private void DeleteLocalizationFolder(string directory) {
        if (string.IsNullOrEmpty(directory)) {
            Debug.LogError("Directory cant be null or empty");
        }

        if (Directory.Exists(directory)) {
            Directory.Delete(directory, true);
            Debug.Log($"Directory was deleted '{directory}'");
        }
        else {
            Debug.Log($"Directory '{directory}' not exist");
        }
    }

    private void CreateLocalizationTable(int index) {
        string _subdirectory = _mainDirectory + $"Dialog{index}";
        string _tableName = $"{_npcNameProperty.stringValue}Dialog{index}";
        _localizationTable = new LocalizationTable(_subdirectory, _tableName);
    }

    private void AddEntriesForProperty(SerializedProperty property) {
        string _playerWords = property.FindPropertyRelative("_playerWords").stringValue;
        _localizationTable.AddEntry("playerWords", _playerWords);

        bool _isNeedNpcWords = property.FindPropertyRelative("_isNeedNpcWords").boolValue;

        if (_isNeedNpcWords) {
            SerializedProperty _npcWordsProperty = property.FindPropertyRelative("_npcWords");
            AddEntriesForArray("npcWords", _npcWordsProperty, _localizationTable);
        }

        bool _isNeedQuest = property.FindPropertyRelative("_isNeedQuest").boolValue;

        if (_isNeedQuest) {
            SerializedProperty _playerWordsAfterQuestCompleteProperty = property.FindPropertyRelative("_playerWordsAfterQuestComplete");
            _localizationTable.AddEntry("playerWordsAfterQuestComplete", _playerWordsAfterQuestCompleteProperty.stringValue);

            SerializedProperty _npcWordsAfterQuestCompleteProperty = property.FindPropertyRelative("_npcWordsAfterQuestComplete");
            AddEntriesForArray("npcWordsAfterQuestComplete", _npcWordsAfterQuestCompleteProperty, _localizationTable);
        }
    }

    private void AddEntriesForArray(string key, SerializedProperty property, LocalizationTable localizationTable) {
        int _count = property.arraySize;

        for (int i = 0; i < _count; i++) {
            localizationTable.AddEntry($"{key}{i}", property.GetArrayElementAtIndex(i).stringValue);
        }
    }
}
