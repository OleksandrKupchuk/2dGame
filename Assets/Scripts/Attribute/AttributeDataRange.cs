using UnityEngine;

[CreateAssetMenu(fileName = "AttributeDataRange", menuName = "AttributeData/AttributeDataRange", order = 2)]
public class AttributeDataRange : AttributeData {
    [SerializeField]
    private int _minValueMinRange;
    [SerializeField]
    private int _maxValueMinRange;
    [SerializeField]
    private int _minValueMaxRange;
    [SerializeField]
    private int _maxValueMaxRange;

    public int ValueMin { get; private set; }
    public int ValueMax { get; private set; }

    private void OnEnable() {
        GenerateParameters();
    }

    public override void GenerateParameters() {
        ValueMin = Random.Range(_minValueMinRange, _maxValueMinRange);
        ValueMax = Random.Range(_minValueMaxRange, _maxValueMaxRange);
    }

    public override string GetValue() {
        string _value = "";

        if (ValueType.Equals(ValueType.Integer)) {
            if (ValueMin > 0) {
                _value += $"<color=green>{ValueMin}</color>";
            }
            else {
                _value += $"<color=red>{ValueMin}</color>";
            }

            if (ValueMax > 0) {
                _value += $"—<color=green>{ValueMax}</color>";
            }
            else {
                _value += $"—<color=red>({ValueMax})</color>";
            }
        }
        else {
            if (ValueMin > 0) {
                _value += $"<color=green>{ValueMin}</color>";
            }
            else {
                _value += $"<color=red>{ValueMin}</color>";
            }

            if (ValueMax > 0) {
                _value += $"—<color=green>{ValueMax}%</color>";
            }
            else {
                _value += $"—<color=red>{ValueMax}%</color>";
            }
        }

        return _value;
    }
}
