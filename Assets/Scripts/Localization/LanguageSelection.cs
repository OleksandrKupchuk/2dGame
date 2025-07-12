using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelection : MonoBehaviour {
    [SerializeField]
    private TMP_Dropdown _dropdown;

    public TMP_Dropdown Dropdown { get => _dropdown; }   

    private void Start() {
        StartCoroutine(SetAvailableLocalizationForDropdown());

        _dropdown.onValueChanged.AddListener(index => {
            StartCoroutine(SetLocale(index));
        });
    }

    private IEnumerator SetAvailableLocalizationForDropdown() {
        yield return LocalizationSettings.InitializationOperation;
        List<string> _localizations = LocalizationSettings.AvailableLocales.Locales
            .Select(locale => locale.Formatter.ToString())
            .ToList();
        _dropdown.ClearOptions();
        _dropdown.AddOptions(_localizations);
    }

    public IEnumerator SetLocale(int index) {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        _dropdown.value = index;
    }
}
