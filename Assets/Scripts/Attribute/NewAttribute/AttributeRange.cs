using UnityEngine;

[CreateAssetMenu(fileName = "AttributeRange", menuName = "Attributes/AttributeRange")]
public class AttributeRange : AttributeBase {
    private float _valueIntegerMin;
    private float _valueIntegerMax;
    private float _valueFromPercentMin;
    private float _valueFromPercentMax;
    private float _valueMinPercent;
    private float _valueMaxPercent;
    private float _damageMin;
    private float _damageMax;

    public float DamageMin {
        get {
            if (_valueIntegerMin + _valueFromPercentMin < 0) {
                return 0;
            }
            else {
                return _damageMin = _valueIntegerMin + _valueFromPercentMin;
            }
        }
    }
    public float DamageMax {
        get {
            if (_valueIntegerMax + _valueFromPercentMax < 0) {
                return 0;
            }
            else {
                return _damageMax = _valueIntegerMax + _valueFromPercentMax;
            }
        }
    }

    public override string GetValueString() {
        return $"{DamageMin}-{DamageMax}";
    }

    protected void OnEnable() {
        ResetData();
        EventManager.OnItemDressed += AddItemAttributes;
        EventManager.TakeAwayItem += SubtractItemAttributes;

        switch (Type) {
            case AttributeType.PhysicalDamage:
                _valueIntegerMin = _playerConfig.PhysicalDamageMin;
                _valueIntegerMax = _playerConfig.PhysicalDamageMax;
                break;
            default:
                Debug.LogError($"Attribute type {Type} is not supported in AttributeRange.");
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

        if (DamageMin < 0) {
            _damageMin = 0;
        }

        if (_damageMax < 0) {
            _damageMax = 0;
        }

        CheckAttributeChange(itemData);
    }

    private void AddIntegerAttributes(ItemData itemData) {
        foreach (AttributeDataBase attribute in itemData.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Integer) {
                AttributeDataRange _attributeData = attribute as AttributeDataRange;
                _valueIntegerMin += _attributeData.ValueMin;
                _valueIntegerMax += _attributeData.ValueMax;
            }
        }
    }

    private void AddPercentAttributes(ItemData itemData) {
        foreach (AttributeDataBase attribute in itemData.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Percent) {
                AttributeDataRange _attributeData = attribute as AttributeDataRange;
                _valueMinPercent += _attributeData.ValueMin;
                _valueMaxPercent += _attributeData.ValueMax;
            }
        }

        CalculationIntegerValueFromPercent();
    }

    private void CalculationIntegerValueFromPercent() {
        _valueFromPercentMin = _valueMinPercent * _valueIntegerMin / 100;
        _valueFromPercentMax = _valueMaxPercent * _valueIntegerMax / 100;
    }

    protected override void CheckAttributeChange(ItemData item) {
        foreach (AttributeDataBase attributeData in item.AttributeDataBase) {
            if (attributeData.AttributeType == Type) {
                EventManager.OnAttributeChangedHandler(Type);
            }
        }
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
                AttributeDataRange _attributeData = attribute as AttributeDataRange;
                _valueIntegerMin -= _attributeData.ValueMin;
                _valueIntegerMax -= _attributeData.ValueMax;
            }
        }
    }

    private void SubtractPercentAttributes(ItemData itemData) {
        foreach (AttributeDataBase attribute in itemData.AttributeDataBase) {
            if (attribute.AttributeType == Type && attribute.ValueType == ValueType.Percent) {
                AttributeDataRange _attributeData = attribute as AttributeDataRange;
                _valueMinPercent -= _attributeData.ValueMin;
                _valueMaxPercent -= _attributeData.ValueMax;
            }
        }

        CalculationIntegerValueFromPercent();
    }

    protected override void ResetData() {
        _valueIntegerMax = 0;
        _valueFromPercentMin = 0;
        _valueIntegerMin = 0;
        _valueFromPercentMax = 0;
        _valueMinPercent = 0;
        _valueMaxPercent = 0;
    }
}
