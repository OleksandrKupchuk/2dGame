using UnityEngine;

[CreateAssetMenu(fileName = "DamageAttribute", menuName = "Attributes/Damage")]
public class DamageAttribute : Attribute {
    [SerializeField]
    private float _valueIntegerMin;
    [SerializeField]
    private float _valueIntegerMax;
    [SerializeField]
    private float _valuePercentMin;
    [SerializeField]
    private float _valuePercentMax;
    [SerializeField]
    private float _valueTemporaryMin;
    [SerializeField]
    private float _valueTemporaryMax;

    public float Damage => Random.Range(DamageMin, DamageMax);

    public float DamageMin { get => _valueIntegerMin + _valuePercentMin + _valueTemporaryMin; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax + _valueTemporaryMax; }
    public override string ValueString => $"{DamageMin}-{DamageMax}";
    public override bool IsValueTemporary => _valueTemporaryMin > 0 || _valueTemporaryMax > 0;

    private new void OnEnable() {
        base.OnEnable();
        AttributeType = AttributeType.Damage;
        _valueIntegerMin = _playerConfig.DamageMin;
        _valueIntegerMax = _playerConfig.DamageMax;
    }

    protected override void AddIntegerAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin += attribute.valueMin;
                _valueIntegerMax += attribute.valueMax;
            }
        }
    }

    protected override void AddPercentAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute += attribute.Value;
            }
        }

        CalculationPercent();
    }

    protected override void SubtractIntegerAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin -= attribute.valueMin;
                _valueIntegerMax -= attribute.valueMax;
            }
        }
    }

    protected override void SubtractPercentAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute -= attribute.Value;
            }
        }

        CalculationPercent();
    }

    private void CalculationPercent() {
        _valuePercentMin = _percentOfAttribute * _valueIntegerMin / 100;
        _valuePercentMax = _percentOfAttribute * _valueIntegerMax / 100;
    }

    protected override void AddTemporaryAttribute(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporaryMin += attribute.valueMin;
                _valueTemporaryMax += attribute.valueMax;
                CheckAttributeChange(itemData);
                return;
            }
        }
    }

    protected override void SubtractTemporaryAttribute(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporaryMin -= attribute.valueMin;
                _valueTemporaryMax -= attribute.valueMax;
                CheckAttributeChange(itemData);
                return;
            }
        }
    }

    protected override void ResetData() {
        base.ResetData();
        _valueIntegerMin = 0;
        _valueIntegerMax = 0;
        _valuePercentMin = 0;
        _valuePercentMax = 0;
        _valueTemporaryMin = 0;
        _valueTemporaryMax = 0;
    }
}
