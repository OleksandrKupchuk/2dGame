using UnityEngine;

[CreateAssetMenu(fileName = "AttributeDataInteger", menuName = "AttributeData/AttributeDataInteger", order = 1)]
public class AttributeDataInteger : AttributeDataBase {
    [SerializeField]
    private int _minValue;
    [SerializeField]
    private int _maxValue;

    public int Value { get; private set; }

    private void OnEnable() {
        GenerateParameters();
    }

    public override void GenerateParameters() {
        Value = Random.Range(_minValue, _maxValue);
    }

    public override string GetValue() {
        if (ValueType.Equals(ValueType.Integer)) {
            if (Value > 0) {
                return $"<color=green>+{Value}</color>";
            }
            else {
                return $"<color=red>-{Value}</color>";
            }
        }
        else {
            if (Value > 0) {
                return $"<color=green>+{Value}%</color>";
            }
            else {
                return $"<color=red>-{Value}%</color>";
            }
        }
    }
}
