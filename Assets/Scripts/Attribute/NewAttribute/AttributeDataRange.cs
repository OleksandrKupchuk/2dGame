using UnityEngine;

[CreateAssetMenu(fileName = "AttributeDataRange", menuName = "AttributeData/AttributeDataRange", order = 2)]
public class AttributeDataRange : AttributeDataBase {
    [SerializeField]
    private float _minValueMinRange;
    [SerializeField]
    private float _maxValueMinRange;
    [SerializeField]
    private float _minValueMaxRange;
    [SerializeField]
    private float _maxValueMaxRange;

    public float ValueMin { get; private set; }
    public float ValueMax { get; private set; }

    public override string GetValue() {
        string _value = "";
        if (ValueType.Equals(ValueType.Integer)) {
            if (ValueMin > 0) {
                _value += $"<color=green>(+{ValueMin})</color>";
            }
            else {
                _value += $"<color=red>(-{ValueMin})</color>";
            }

            if (ValueMax > 0) {
                _value += $"<color=green>-(+{ValueMax})</color>";
            }
            else {
                _value += $"<color=red>-(-{ValueMax})</color>";
            }
        }
        else {
            if (ValueMin > 0) {
                _value += $"<color=green>(+{ValueMin}%)</color>";
            }
            else {
                _value += $"<color=red>(-{ValueMin}%)</color>";
            }

            if (ValueMax > 0) {
                _value += $"<color=green>-(+{ValueMax}%)</color>";
            }
            else {
                _value += $"<color=red>-(-{ValueMax}%)</color>";
            }
        }

        return _value;
    }

    public override void GenerateParameters() {
        ValueMin = Random.Range(_minValueMinRange, _maxValueMinRange);
        ValueMax = Random.Range(_minValueMaxRange, _maxValueMaxRange);
    }
}
