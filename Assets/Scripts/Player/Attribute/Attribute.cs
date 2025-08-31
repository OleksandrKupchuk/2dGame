using System;
using UnityEngine;

public abstract class Attribute : ScriptableObject {
    [SerializeField]
    protected AttributeConfig _config;
    [SerializeField]
    protected bool _isRangeAttribute;

    public bool IsRangeAttribute => _isRangeAttribute;
    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; } = new AttributeType();
    public event Action OnAttributeChanged;
    public void OnAttributeChangedHandler() => OnAttributeChanged?.Invoke();

    public abstract bool IsEmptyValue();
    public abstract string GetValueString();
    protected abstract void AddItemAttributes(Item itemData);
    protected abstract void SubtractItemAttributes(Item itemData);
    protected abstract void CheckAttributeChange(Item itemData);
    protected abstract void ResetData();
}
