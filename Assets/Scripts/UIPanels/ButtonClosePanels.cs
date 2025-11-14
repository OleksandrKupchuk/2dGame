using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClosePanels : MonoBehaviour {
    [SerializeField]
    private Button _closeButton;

    public event Action OnClosePanels;

    private void Awake() {
        _closeButton.onClick.AddListener(() => { 
            OnClosePanels?.Invoke();
            _closeButton.gameObject.SetActive(false);
        });
    }

    public void ShowCloseButton() {
        _closeButton?.gameObject.SetActive(true);
    }

    public void HideCloseButton() {
        _closeButton?.gameObject.SetActive(false);
    }
}
