using UnityEngine;

public abstract class AttributeBase : ScriptableObject {
    [SerializeField]
    protected PlayerConfig _playerConfig;

    [field: SerializeField]
    public AttributeType Type { get; protected set; } = new AttributeType();

    public abstract string GetValueString();
    protected abstract void AddItemAttributes(ItemData itemData);
    protected abstract void SubtractItemAttributes(ItemData itemData);
    protected abstract void CheckAttributeChange(ItemData itemData);
    protected abstract void ResetData();
}
