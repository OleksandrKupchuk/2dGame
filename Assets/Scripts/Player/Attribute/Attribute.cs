using System;
using UnityEngine;

public abstract class Attribute : ScriptableObject {
    [SerializeField]
    protected AttributeConfig _config;
    [SerializeField]
    protected bool _isRangeAttribute;

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; } = new AttributeType();
    public event Action OnAttributeChanged;
    public void OnAttributeChangedHandler() => OnAttributeChanged?.Invoke();

<<<<<<< HEAD:Assets/Scripts/Player/Attribute/Attribute.cs
    public abstract bool IsEmptyValue();
=======
    public event Action OnAttributeChanged;
    public bool IsRangeAttribute => _isRangeAttribute;

    public void OnAttributeChangedHandler() {
        OnAttributeChanged.Invoke();
    }

>>>>>>> d84f61f28eae6158f881ef62479366b326da010c:Assets/Scripts/Player/Attribute/PlayerAttribute.cs
    public abstract string GetValueString();
    protected abstract void AddItemAttributes(Item itemData);
    protected abstract void SubtractItemAttributes(Item itemData);
    protected abstract void CheckAttributeChange(Item itemData);
    protected abstract void ResetData();
}
