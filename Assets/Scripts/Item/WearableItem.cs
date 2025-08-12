using UnityEngine;

[CreateAssetMenu(fileName = "WearableItem", menuName = "Item/WearableItem")]
public class WearableItem : Item {
    [SerializeField]
    private ItemTypeAttribute _itemTypeAttribute;
    [SerializeField]
    private BodyType _bodyType;

    public ItemTypeAttribute ItemTypeAttribute => _itemTypeAttribute;
    public BodyType BodyType => _bodyType;
}

public enum ItemTypeAttribute {
    Helmet,
    Armor,
    Ring,
    Amulet,
    Weapon,
    Belt,
    Shield
}
