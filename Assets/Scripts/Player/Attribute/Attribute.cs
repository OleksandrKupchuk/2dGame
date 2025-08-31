using System;
using UnityEngine;

public abstract class Attribute : ScriptableObject {
    [SerializeField]
    protected bool _isRangeAttribute;
    [SerializeField]
    protected PlayerConfig _playerConfig;

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; } = new AttributeType();

    public event Action OnAttributeChanged;
    public bool IsRangeAttribute => _isRangeAttribute;

    public void OnAttributeChangedHandler() {
        OnAttributeChanged.Invoke();
    }

    public abstract string GetValueString();
    protected abstract void AddItemAttributes(Item itemData);
    protected abstract void SubtractItemAttributes(Item itemData);
    protected abstract void CheckAttributeChange(Item itemData);
    protected abstract void ResetData();
}
