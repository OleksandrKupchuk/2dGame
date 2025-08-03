using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributeRange", menuName = "PlayerAttribute/Range")]
public class PlayerAttributeRange : PlayerAttribute {
    private float _valueIntegerMin;
    private float _valueIntegerMax;
    private float _valueFromPercentMin;
    private float _valueFromPercentMax;
    private float _valueMinPercent;
    private float _valueMaxPercent;

    public float DamageMin => _valueIntegerMin + _valueFromPercentMin;
    public float DamageMax => _valueIntegerMax + _valueFromPercentMax;
    public float Damage => Random.Range(DamageMin, DamageMax);

    public override string GetValueString() {
        return $"{DamageMin}-{DamageMax}";
    }

    protected void OnEnable() {
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
            case AttributeType.PhysicalDamage:
                _valueIntegerMin = _playerConfig.PhysicalDamageMin;
                _valueIntegerMax = _playerConfig.PhysicalDamageMax;
                break;
            default:
                Debug.LogError($"PlayerAttribute type {AttributeType} is not supported in PlayerAttributeRange.");
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
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Integer) {
                AttributeDataRange _attributeData = attribute as AttributeDataRange;
                _valueIntegerMin += _attributeData.ValueMin;
                _valueIntegerMax += _attributeData.ValueMax;
            }
        }
    }

    private void AddPercentAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
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
        foreach (AttributeData attributeData in item.Attributes) {
            if (attributeData.AttributeType == AttributeType) {
                EventManager.OnAttributeChangedHandler(AttributeType);
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
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Integer) {
                AttributeDataRange _attributeData = attribute as AttributeDataRange;
                _valueIntegerMin -= _attributeData.ValueMin;
                _valueIntegerMax -= _attributeData.ValueMax;
            }
        }
    }

    private void SubtractPercentAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
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
