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
    private Attribute _attribute;
    [SerializeField]
    private AttributeBase _attributeBase;

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
        if (_attributeBase.Type != type) {
            return;
        }
        else {
            _value.text = $"{_attributeBase.GetValueString()}";
        }
    }
}
