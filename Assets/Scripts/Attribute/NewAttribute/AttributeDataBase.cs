using UnityEngine;

public class AttributeDataBase : ScriptableObject {
    [SerializeField]
    private AttributeType _type = new AttributeType();
    [SerializeField]
    public ValueType _valueType = new ValueType();
    [field: SerializeField]
    public Sprite Icon { get; private set; }

    public AttributeType Type => _type;
    public ValueType ValueType => _valueType;
    public virtual string GetValue() { return ""; }
    public virtual void GenerateParameters() { }
}
