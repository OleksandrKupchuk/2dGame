using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributeInteger", menuName = "PlayerAttribute/Integer")]
public class PlayerAttributeInteger : PlayerAttribute {
    private float _valueInteger;
    private float _valuePercent;
    private float _valueFromPercent;

    public float Value { get => _valueInteger + _valueFromPercent; }

    private void OnEnable() {
        ResetData();
        EventManager.OnItemDressed += AddItemAttributes;
        EventManager.TakeAwayItem += SubtractItemAttributes;
        CheckAttributeType();
    }

    private void OnValidate() {
        CheckAttributeType();
    }

    private void CheckAttributeType() {
        switch (AttributeType) {
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
                Debug.LogError($"PlayerAttribute type {AttributeType} is not supported in PlayerAttributeInteger.");
                break;
        }
    }

    private void OnDisable() {
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
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Integer) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valueInteger += _attributeData.Value;
            }
        }
    }

    private void AddPercentAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
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
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Integer) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valueInteger -= _attributeData.Value;
            }
        }
    }

    private void SubtractPercentAttributes(ItemData item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
                AttributeDataInteger _attributeData = attribute as AttributeDataInteger;
                _valuePercent -= _attributeData.Value;
            }
        }

        _valueFromPercent = _valuePercent * _valueInteger / 100;
    }

    protected override void CheckAttributeChange(ItemData item) {
        foreach (AttributeData attributeData in item.Attributes) {
            if (attributeData.AttributeType == AttributeType) {
                EventManager.OnAttributeChangedHandler(AttributeType);
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
