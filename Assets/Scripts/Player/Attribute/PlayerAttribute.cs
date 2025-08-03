using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerAttribute : ScriptableObject {
    [SerializeField]
    protected PlayerConfig _playerConfig;

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; } = new AttributeType();

    public abstract string GetValueString();
    protected abstract void AddItemAttributes(ItemData itemData);
    protected abstract void SubtractItemAttributes(ItemData itemData);
    protected abstract void CheckAttributeChange(ItemData itemData);
    protected abstract void ResetData();
}
