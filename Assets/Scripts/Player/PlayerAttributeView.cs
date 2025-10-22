using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributeView : MonoBehaviour {
    [SerializeField]
    protected Sprite _spriteAttribute;
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Text _value;
    [SerializeField]
    private CharacterAttribute _playerAttribute;

    private void Awake() {
        _playerAttribute.OnAttributeChanged += UpdateAttributeView;
    }

    private void OnDestroy() {
        _playerAttribute.OnAttributeChanged -= UpdateAttributeView;
    }

    private void Start() {
        _icon.sprite = _spriteAttribute;
        UpdateAttributeView();
    }

    private void UpdateAttributeView() {
        _value.text = $"{_playerAttribute.GetValueString()}";
    }
}
