using UnityEngine;

[CreateAssetMenu(fileName = "ItemData AttributeData", menuName = "ItemData AttributeData/AttributeData", order = 1)]
public class AttributeData : ScriptableObject {
    public AttributeType type = new AttributeType();
    public ValueType valueType = new ValueType();
    public float value;
    public float valueMin;
    public float valueMax;
    public Sprite icon;

    public string GetValue() {
        if (value > 0) {
            if (valueType == ValueType.Integer) {
                return $"+{value}";
            }
            else {
                return $"+{value}%";
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
