using UnityEngine;
using UnityEngine.UI;

public class SettingsPage : MonoBehaviour {
    private string _settingFileName = "Settings";

    [SerializeField]
    private Button _apply;
    [SerializeField]
    private Button _load;
    [SerializeField]
    private LanguageSelection _languageSelection;

    private void Awake() {
        _apply.onClick.AddListener(() => {
            SaveSettings();
        });

        _load.onClick.AddListener(() => {
            LoadSettings();
        });
    }

    private void SaveSettings() {
        SaveLoadJsonSystem saveLoadSystem = new SaveLoadJsonSystem();
        SettingsData _settingsData = new SettingsData();
        _settingsData.languageIndex = _languageSelection.Dropdown.value;
        saveLoadSystem.Save(_settingFileName, _settingsData);
        print("localizationIndex = " + _settingsData.languageIndex);
    }

    private void LoadSettings() {
        SaveLoadJsonSystem saveLoadSystem = new SaveLoadJsonSystem();
        SettingsData _settingsData = saveLoadSystem.Load<SettingsData>(_settingFileName);
        print(JsonUtility.ToJson(_settingsData, true));
        StartCoroutine(_languageSelection.SetLocale(_settingsData.languageIndex));
    }
}
