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

    private void Awake() {
        //EventManager.OnAttributeChanged += UpdateAttributeView;
        _attribute.OnAttributeChanged += UpdateAttributeView;
    }

    private void OnDestroy() {
        //EventManager.OnAttributeChanged -= UpdateAttributeView;
        _attribute.OnAttributeChanged -= UpdateAttributeView;
    }

    private void Start() {
        _icon.sprite = _spriteAttribute;
        UpdateAttributeView();
    }

    private void UpdateAttributeView(AttributeType type) {
        if (_attribute.AttributeType != type) {
            return;
        }
        else {
            _value.text = $"{_attribute.GetValueString()}";
        }
    }

    private void UpdateAttributeView() {
        _value.text = $"{_attribute.GetValueString()}";
    }
}
