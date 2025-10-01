using UnityEngine;

[CreateAssetMenu(fileName = "ItemAttributeInteger", menuName = "ItemAttribute/ItemAttributeInteger")]
public class ItemAttributeInteger : ItemAttribute {
    [SerializeField]
    private float _minValue;
    [SerializeField]
    private float _maxValue;

    public float Value { get; set; }

    private void Awake() {
        _isRangeAttribute = false;
    }

    public override void GenerateParameters() {
        Value = Random.Range(_minValue, _maxValue);
    }

    public override string GetValue() {
        if (ValueType.Equals(ValueType.Integer)) {
            if (Value > 0) {
                return $"<color=green>+{string.Format("{0:0.0}", Value)}</color>";
            }
            else {
                return $"<color=red>-{string.Format("{0:0.0}", Value)}</color>";
            }
        }
        else {
            if (Value > 0) {
                return $"<color=green>+{string.Format("{0:0.0}", Value)}%</color>";
            }
            else {
                return $"<color=red>-{string.Format("{0:0.0}", Value)}%</color>";
            }
        }
    }
}
