using UnityEngine;

[System.Serializable]
public class ItemAttribute {
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private ValueForm _valueForm;
    [SerializeField]
    private ModifierType _modifierType;
    [SerializeField]
    private AttributeType _attributeType;
    [SerializeField]
    private float _fixedMin;
    [SerializeField]
    private float _fixedMax;
    [SerializeField]
    private float _rangeMinLower;
    [SerializeField]
    private float _rangeMaxLower;
    [SerializeField]
    private float _rangeMinUpper;
    [SerializeField]
    private float _rangeMaxUpper;

    public Sprite Icon { get => _icon; set => _icon = value; }
    public ValueForm ValueForm { get => _valueForm; set => _valueForm = value; }
    public ModifierType ModifierType { get => _modifierType; set => _modifierType = value; }
    public AttributeType AttributeType { get => _attributeType; set => _attributeType = value; }
    public float FixedValue { get; set; }
    public float RangeMinValue { get; set; }
    public float RangeMaxValue { get; set; }

    public void GenerateParameters() {
        if (ValueForm == ValueForm.Fixed) {
            FixedValue = Random.Range(_fixedMin, _fixedMax);
            Debug.Log($"Generated Fixed Value: {FixedValue}, <color=green>{AttributeType}</color>");
        }
        else {
            RangeMinValue = Random.Range(_rangeMinLower, _rangeMaxLower);
            RangeMaxValue = Random.Range(_rangeMinUpper, _rangeMaxUpper);
            Debug.Log($"Generated Range Min Value: {RangeMinValue}, <color=green>{AttributeType}</color>");
            Debug.Log($"Generated Range Max Value: {RangeMaxValue}, <color=green>{AttributeType}</color>");
        }
    }

    public string GetValue() {
        if (ValueForm.Equals(ValueForm.Fixed)) {
            if (ModifierType.Equals(ModifierType.Integer)) {
                if (FixedValue > 0) {
                    return $"<color=green>+{string.Format("{0:0.0}", FixedValue)}</color>";
                }
                else {
                    return $"<color=red>-{string.Format("{0:0.0}", FixedValue)}</color>";
                }
            }
            else {
                if (FixedValue > 0) {
                    return $"<color=green>+{string.Format("{0:0.0}", FixedValue)}%</color>";
                }
                else {
                    return $"<color=red>-{string.Format("{0:0.0}", FixedValue)}%</color>";
                }
            }
        }
        else {
            string _value = "";

            if (ModifierType.Equals(ModifierType.Integer)) {
                if (RangeMinValue > 0) {
                    _value += $"<color=green>{string.Format("{0:0.0}", RangeMinValue)}</color>";
                }
                else {
                    _value += $"<color=red>{string.Format("{0:0.0}", RangeMinValue)}</color>";
                }

                if (RangeMaxValue > 0) {
                    _value += $"—<color=green>{string.Format("{0:0.0}", RangeMaxValue)}</color>";
                }
                else {
                    _value += $"—<color=red>({string.Format("{0:0.0}", RangeMaxValue)}) </color>";
                }
            }
            else {
                if (RangeMinValue > 0) {
                    _value += $"<color=green>{string.Format("{0:0.0}", RangeMinValue)}</color>";
                }
                else {
                    _value += $"<color=red>{string.Format("{0:0.0}", RangeMinValue)}</color>";
                }

                if (RangeMaxValue > 0) {
                    _value += $"—<color=green>{string.Format("{0:0.0}", RangeMaxValue)}%</color>";
                }
                else {
                    _value += $"—<color=red>{string.Format("{0:0.0}", RangeMaxValue)}%</color>";
                }
            }

            return _value;
        }
    }

    private string GetFormatedValue(float value, Color color) {
        return $"<color={color}>{string.Format("{0:0.0}", value)}</color>";
    }
}

public enum ValueForm {
    Fixed,
    Range
}

public enum ModifierType {
    Integer,
    Percent
}

public enum AttributeType {
    Armor,
    Health,
    Speed,
    HealthRegeneration,
    PhysicalDamage,
    FireDamage,
    FrostDamage,
    LightingDamage,
    PoisonDamage,
    MagicDamage,
    FireResistance,
    Test
}
