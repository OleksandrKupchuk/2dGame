using UnityEngine;

[CreateAssetMenu(fileName = "ItemData AttributeData", menuName = "ItemData AttributeData/AttributeData", order = 1)]
public class AttributeData : ScriptableObject {
    [SerializeField]
    private float _minValueRange;
    [SerializeField]
    private float _maxValueRange;
    [SerializeField]
    private float _minValueMinRange;
    [SerializeField]
    private float _maxValueMinRange;
    [SerializeField]
    private float _minValueMaxRange;
    [SerializeField]
    private float _maxValueMaxRange;

    public AttributeType type = new AttributeType();
    public ValueType valueType = new ValueType();
    public float Value { get; private set; }
    public float valueMin;
    public float valueMax;
    public Sprite icon;

    public string GetValue() {
        if (Value > 0) {
            if (valueType == ValueType.Integer) {
                return $"+{Value}";
            }
            else {
                return $"+{Value}%";
            }
        }
        else {
            if (valueType == ValueType.Integer) {
                return $"+{valueMin}-{valueMax}";
            }
            else {
                return $"+{valueMin}-{valueMax}%";
            }
        }
    }

    public void GenerateValue() {
        Value = Random.Range(_minValueRange, _maxValueRange);
    }
}

public enum AttributeType {
    Armor,
    Damage,
    Health,
    Speed,
    HealthRegeneration
}

public enum ValueType {
    Integer,
    Percent
}
