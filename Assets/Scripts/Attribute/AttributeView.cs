using UnityEngine;
using UnityEngine.UI;

public class AttributeView : MonoBehaviour {
    [SerializeField]
    protected Sprite _spriteAttribute;
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Text _value;
    [SerializeField]
    private PlayerAttribute _attribute;

    private void Awake() {
        EventManager.OnAttributeChanged += UpdateAttributeView;
    }

    private void OnDestroy() {
        EventManager.OnAttributeChanged -= UpdateAttributeView;
    }

    private void Start() {
        _icon.sprite = _spriteAttribute;
        UpdateAttributeView(_attribute.AttributeType);
    }

    private void UpdateAttributeView(AttributeType type) {
        if (_attribute.AttributeType != type) {
            return;
        }
        else {
            _value.text = $"{_attribute.GetValueString()}";
        }
    }
}
