using UnityEngine;

[CreateAssetMenu(fileName = "CharacterRangeAttribute", menuName = "Character/RangeAttribute")]
public class CharacterRangeAttribute : CharacterAttribute {
    private float _valueIntegerMin;
    private float _valueIntegerMax;
    private float _valueFromPercentMin;
    private float _valueFromPercentMax;
    private float _valueMinPercent;
    private float _valueMaxPercent;

    private float DamageMin => _valueIntegerMin + _valueFromPercentMin;
    private float DamageMax => _valueIntegerMax + _valueFromPercentMax;

    public float Damage => Random.Range(DamageMin, DamageMax);

    public override string GetValueString() {
        return $"{string.Format("{0:0.0}", DamageMin)}-{string.Format("{0:0.0}", DamageMax)}";
    }

    protected void OnEnable() {
        ResetData();
        EventManager.OnItemDressed += AddItemAttributes;
        EventManager.TakeAwayItem += SubtractItemAttributes;
        CheckAttributeType();
        _isRangeAttribute = true;
    }

    private void OnValidate() {
        CheckAttributeType();
    }

    private void CheckAttributeType() {
        switch (AttributeType) {
            case AttributeType.PhysicalDamage:
                _valueIntegerMin = _config.PhysicalDamageMin;
                _valueIntegerMax = _config.PhysicalDamageMax;
                break;
            case AttributeType.FireDamage:
                _valueIntegerMin = _config.FireDamageMin;
                _valueIntegerMax = _config.FireDamageMax;
                break;
            default:
                Debug.LogError($"CharacterAttribute type {AttributeType} is not supported in CharacterRangeAttribute.");
                break;
        }
    }

    protected void OnDisable() {
        EventManager.OnItemDressed -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubtractItemAttributes;
    }

    protected override void AddItemAttributes(Item itemData) {
        if (itemData == null) {
            return;
        }

        AddIntegerAttributes(itemData);
        AddPercentAttributes(itemData);

        OnAttributeChangedHandler();
    }

    private void AddIntegerAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ModifierType == ModifierType.Integer) {
                _valueIntegerMin += attribute.RangeMinValue;
                _valueIntegerMax += attribute.RangeMaxValue;
            }
        }
    }

    private void AddPercentAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ModifierType == ModifierType.Percent) {
                _valueMinPercent += attribute.RangeMinValue;
                _valueMaxPercent += attribute.RangeMaxValue;
            }
        }

        CalculationIntegerValueFromPercent();
    }

    private void CalculationIntegerValueFromPercent() {
        _valueFromPercentMin = _valueMinPercent * _valueIntegerMin / 100;
        _valueFromPercentMax = _valueMaxPercent * _valueIntegerMax / 100;
    }

    protected override void SubtractItemAttributes(Item itemData) {
        if (itemData == null) {
            return;
        }

        SubtractIntegerAttributes(itemData);
        SubtractPercentAttributes(itemData);

        OnAttributeChangedHandler();
    }

    private void SubtractIntegerAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ModifierType == ModifierType.Integer) {
                _valueIntegerMin -= attribute.RangeMinValue;
                _valueIntegerMax -= attribute.RangeMaxValue;
            }
        }
    }

    private void SubtractPercentAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ModifierType == ModifierType.Percent) {
                _valueMinPercent -= attribute.RangeMinValue;
                _valueMaxPercent -= attribute.RangeMaxValue;
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
