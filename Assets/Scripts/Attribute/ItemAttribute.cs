using UnityEngine;

[System.Serializable]
public abstract class ItemAttribute : ScriptableObject {
    [SerializeField]
    protected bool _isRangeAttribute;
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected AttributeType _attributeType;
    [SerializeField]
    protected ValueType _valueType;

    public bool IsRangeAttribute => _isRangeAttribute;
    public AttributeType AttributeType => _attributeType;
    public ValueType ValueType => _valueType;
    public Sprite Icon => _icon;

    protected void OnEnable() {
        GenerateParameters();
    }

    public abstract void GenerateParameters();

    public abstract string GetValue();
}

public enum AttributeType {
    Armor,
    Health,
    Speed,
    HealthRegeneration,
    PhysicalDamage,
    FireDamage,
    FrostDamage,
    PoisonDamage,
    MagicDamage,
}

public enum ValueType {
    Integer,
    Percent
}
