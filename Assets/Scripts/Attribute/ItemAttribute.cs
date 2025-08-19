using System.Collections.Generic;
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

    public bool IsRangeAttribute { get => _isRangeAttribute; set => _isRangeAttribute = value; }
    public AttributeType AttributeType { get => _attributeType; set => _attributeType = value; }
    public ValueType ValueType { get => _valueType; set => _valueType = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }

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
