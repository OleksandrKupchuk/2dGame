using UnityEngine;

[CreateAssetMenu(fileName = "ItemAttributeInteger", menuName = "ItemAttribute/ItemAttributeInteger")]
public class ItemAttributeInteger : ItemAttribute {
    private float _value;

    [SerializeField]
    private float _minValue;
    [SerializeField]
    private float _maxValue;

    public float Value => _value;

    private void Awake() {
        _isRangeAttribute = false;
    }

    public override void GenerateParameters() {
        _value = Random.Range(_minValue, _maxValue);
    }

    public override string GetValue() {
        if (ValueType.Equals(ValueType.Integer)) {
            if (_value > 0) {
                return $"<color=green>+{string.Format("{0:0.0}", _value)}</color>";
            }
            else {
                return $"<color=red>-{string.Format("{0:0.0}", _value)}</color>";
            }
        }
        else {
            if (_value > 0) {
                return $"<color=green>+{string.Format("{0:0.0}", _value)}%</color>";
            }
            else {
                return $"<color=red>-{string.Format("{0:0.0}", _value)}%</color>";
            }
        }
    }
}
