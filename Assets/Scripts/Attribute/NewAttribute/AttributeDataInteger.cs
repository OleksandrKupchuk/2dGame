using UnityEngine;

[CreateAssetMenu(fileName = "AttributeDataInteger", menuName = "AttributeData/AttributeDataInteger", order = 1)]
public class AttributeDataInteger : AttributeDataBase {
    [SerializeField]
    private float _minValueRange;
    [SerializeField]
    private float _maxValueRange;

    [field: SerializeField]
    public float Value { get; private set; }

    public override void GenerateParameters() {
        Value = Random.Range(_minValueRange, _maxValueRange);
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
