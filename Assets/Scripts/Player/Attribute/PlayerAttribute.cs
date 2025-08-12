using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerAttribute : ScriptableObject {
    [SerializeField]
    protected bool _isRangeAttribute;
    [SerializeField]
    protected PlayerConfig _playerConfig;

    public bool IsRangeAttribute => _isRangeAttribute;
    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; } = new AttributeType();

    public abstract string GetValueString();
    protected abstract void AddItemAttributes(Item itemData);
    protected abstract void SubtractItemAttributes(Item itemData);
    protected abstract void CheckAttributeChange(Item itemData);
    protected abstract void ResetData();
}
