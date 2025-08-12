using UnityEngine;

[CreateAssetMenu(fileName = "ItemAttributeRange", menuName = "ItemAttribute/ItemAttributeRange")]
public class ItemAttributeRange : ItemAttribute {
    [SerializeField]
    private float _minValueMinRange;
    [SerializeField]
    private float _maxValueMinRange;
    [SerializeField]
    private float _minValueMaxRange;
    [SerializeField]
    private float _maxValueMaxRange;

    public float ValueMinRange { get; private set; }
    public float ValueMaxRange { get; private set; }

    private void Awake() {
        _isRangeAttribute = true;
    }

    public override void GenerateParameters() {
        ValueMinRange = Random.Range(_minValueMinRange, _maxValueMinRange);
        ValueMaxRange = Random.Range(_minValueMaxRange, _maxValueMaxRange);
    }

    public override string GetValue() {
        string _value = "";

        if (ValueType.Equals(ValueType.Integer)) {
            if (ValueMinRange > 0) {
                _value += $"<color=green>{string.Format("{0:0.0}", ValueMinRange)}</color>";
            }
            else {
                _value += $"<color=red>{string.Format("{0:0.0}", ValueMinRange)}</color>";
            }

            if (ValueMaxRange > 0) {
                _value += $"—<color=green>{string.Format("{0:0.0}", ValueMaxRange)}</color>";
            }
            else {
                _value += $"—<color=red>({string.Format("{0:0.0}", ValueMaxRange)}) </color>";
            }
        }
        else {
            if (ValueMinRange > 0) {
                _value += $"<color=green>{string.Format("{0:0.0}", ValueMinRange)}</color>";
            }
            else {
                _value += $"<color=red>{string.Format("{0:0.0}", ValueMinRange)}</color>";
            }

            if (ValueMaxRange > 0) {
                _value += $"—<color=green>{string.Format("{0:0.0}", ValueMaxRange)}%</color>";
            }
            else {
                _value += $"—<color=red>{string.Format("{0:0.0}", ValueMaxRange)}%</color>";
            }
        }

        return _value;
    }
}
