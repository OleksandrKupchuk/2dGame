using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributeInteger", menuName = "Attribute/Integer")]
public class PlayerAttributeInteger : Attribute {
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
                Debug.LogError($"Attribute type {AttributeType} is not supported in PlayerAttributeInteger.");
                break;
        }
    }

    private void OnDisable() {
        EventManager.OnItemDressed -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubtractItemAttributes;
    }

    protected override void AddItemAttributes(Item itemData) {
        if (itemData == null) {
            return;
        }

        AddIntegerAttributes(itemData);
        AddPercentAttributes(itemData);

        CheckAttributeChange(itemData);
    }

    private void AddIntegerAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Integer) {
                ItemAttributeInteger _attributeData = attribute as ItemAttributeInteger;
                _valueInteger += _attributeData.Value;
            }
        }
    }

    private void AddPercentAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
                ItemAttributeInteger _attributeData = attribute as ItemAttributeInteger;
                _valuePercent += _attributeData.Value;
            }
        }

        _valueFromPercent = _valuePercent * _valueInteger / 100;
    }

    protected override void SubtractItemAttributes(Item itemData) {
        if (itemData == null) {
            return;
        }

        SubtractIntegerAttributes(itemData);
        SubtractPercentAttributes(itemData);

        CheckAttributeChange(itemData);
    }

    private void SubtractIntegerAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Integer) {
                ItemAttributeInteger _attributeData = attribute as ItemAttributeInteger;
                _valueInteger -= _attributeData.Value;
            }
        }
    }

    private void SubtractPercentAttributes(Item item) {
        foreach (ItemAttribute attribute in item.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
                ItemAttributeInteger _attributeData = attribute as ItemAttributeInteger;
                _valuePercent -= _attributeData.Value;
            }
        }

        _valueFromPercent = _valuePercent * _valueInteger / 100;
    }

    protected override void CheckAttributeChange(Item item) {
        foreach (ItemAttribute attributeData in item.Attributes) {
            if (attributeData.AttributeType == AttributeType) {
                OnAttributeChangedHandler();
            }
        }
    }

    protected override void ResetData() {
        _valueInteger = 0;
        _valueFromPercent = 0;
        _valuePercent = 0;
    }

    public override string GetValueString() {
        return string.Format("{0:0.0}", _valueInteger + _valueFromPercent);
    }
}
