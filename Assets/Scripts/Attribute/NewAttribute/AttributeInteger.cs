using UnityEngine;

[CreateAssetMenu(fileName = "AttributeInteger", menuName = "Attributes/AttributeInteger")]
public class AttributeInteger : AttributeBase {
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _valueFromPercent;

    public float Value { get => _valueInteger + _valueFromPercent; }

    protected void OnEnable() {
        ResetData();
        EventManager.OnItemDressed += AddItemAttributes;
        EventManager.TakeAwayItem += SubtractItemAttributes;

        switch (Type) {
            case AttributeType.Health:
                _valueInteger = _playerConfig.Health;
                break;
            case AttributeType.HealthRegeneration:
                _valueInteger = _playerConfig.HealthRegeneration;
                break;
            case AttributeType.Armor:
                _valueInteger = _playerConfig.Armor;
                break;
            case AttributeType.Speed:
                _valueInteger = _playerConfig.Speed;
                break;
            default:
                Debug.LogError($"Attribute type {Type} is not supported in AttributeInteger.");
                break;
        }
    }

    protected void OnDisable() {
        EventManager.OnItemDressed -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubtractItemAttributes;
    }

    protected override void AddItemAttributes(ItemData itemData) {
        if (itemData == null) {
            return;
        }

        AddIntegerAttributes(itemData);
        AddPercentAttributes(itemData);

        CheckAttributeChange(itemData);
    }

    private void AddIntegerAttributes(ItemData itemData) {
        foreach (AttributeDataBase attribute in itemData.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Integer) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valueInteger += _attributeData.Value;
            }
        }
    }

    private void AddPercentAttributes(ItemData itemData) {
        foreach (AttributeDataBase attribute in itemData.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Percent) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valuePercent += _attributeData.Value;
            }
        }

        _valueFromPercent = _valuePercent * _valueInteger / 100;
    }

    protected override void SubtractItemAttributes(ItemData itemData) {
        if (itemData == null) {
            return;
        }

        SubtractIntegerAttributes(itemData);
        SubtractPercentAttributes(itemData);

        CheckAttributeChange(itemData);
    }

    private void SubtractIntegerAttributes(ItemData itemData) {
        foreach (AttributeDataBase attribute in itemData.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Integer) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valueInteger -= _attributeData.Value;
            }
        }
    }

    private void SubtractPercentAttributes(ItemData item) {
        foreach (AttributeDataBase attribute in item.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Percent) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valuePercent -= _attributeData.Value;
            }
        }

        _valueFromPercent = _valuePercent * _valueInteger / 100;
    }

    protected override void CheckAttributeChange(ItemData item) {
        foreach (AttributeDataBase attributeData in item.AttributeDataBase) {
            if (attributeData.AttributeType == Type) {
                EventManager.OnAttributeChangedHandler(Type);
            }
        }
    }

    protected override void ResetData() {
        _valueInteger = 0;
        _valueFromPercent = 0;
        _valuePercent = 0;
    }

    public override string GetValueString() {
        return (_valueInteger + _valueFromPercent).ToString();
    }
}
