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
        return $"{string.Format("{0:0.0}", DamageMin)}-{string.Format("{0:0.0}", DamageMax)}";
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
            case AttributeType.FireDamage:
                _valueIntegerMin = _playerConfig.FireDamageMin;
                _valueIntegerMax = _playerConfig.FireDamageMax;
                break;
            case AttributeType.FrostDamage:
                _valueIntegerMin = _playerConfig.FrostDamageMin;
                _valueIntegerMax = _playerConfig.FrostDamageMax;
                break;
            case AttributeType.PoisonDamage:
                _valueIntegerMin = _playerConfig.PoisonDamageMin;
                _valueIntegerMax = _playerConfig.PoisonDamageMax;
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
                ItemAttributeRange _attributeData = attribute as ItemAttributeRange;
                _valueIntegerMin += _attributeData.ValueMinRange;
                _valueIntegerMax += _attributeData.ValueMaxRange;
            }
        }
    }

    private void AddPercentAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
                ItemAttributeRange _attributeData = attribute as ItemAttributeRange;
                _valueMinPercent += _attributeData.ValueMinRange;
                _valueMaxPercent += _attributeData.ValueMaxRange;
            }
        }

        CalculationIntegerValueFromPercent();
    }

    private void CalculationIntegerValueFromPercent() {
        _valueFromPercentMin = _valueMinPercent * _valueIntegerMin / 100;
        _valueFromPercentMax = _valueMaxPercent * _valueIntegerMax / 100;
    }

    protected override void CheckAttributeChange(Item item) {
        foreach (ItemAttribute attributeData in item.Attributes) {
            if (attributeData.AttributeType == AttributeType) {
                OnAttributeChangedHandler();
            }
        }
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
                ItemAttributeRange _attributeData = attribute as ItemAttributeRange;
                _valueIntegerMin -= _attributeData.ValueMinRange;
                _valueIntegerMax -= _attributeData.ValueMaxRange;
            }
        }
    }

    private void SubtractPercentAttributes(Item itemData) {
        foreach (ItemAttribute attribute in itemData.Attributes) {
            if (attribute.AttributeType == AttributeType && attribute.ValueType == ValueType.Percent) {
                ItemAttributeRange _attributeData = attribute as ItemAttributeRange;
                _valueMinPercent -= _attributeData.ValueMinRange;
                _valueMaxPercent -= _attributeData.ValueMaxRange;
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
