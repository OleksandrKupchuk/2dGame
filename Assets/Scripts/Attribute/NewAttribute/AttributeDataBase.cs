using UnityEngine;

public abstract class AttributeDataBase : ScriptableObject {
    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; } = new AttributeType();
    [field: SerializeField]
    public ValueType ValueType { get; protected set; } = new ValueType();
    [field: SerializeField]
    public Sprite Icon { get; protected set; }
    public abstract string GetValue();
    public abstract void GenerateParameters();
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
