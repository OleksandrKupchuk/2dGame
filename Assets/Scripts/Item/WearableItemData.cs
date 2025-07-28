using UnityEngine;

[CreateAssetMenu(fileName = "WearableItemData", menuName = "ItemData/WearableItemData")]
public class WearableItemData : ItemData {
    [field: SerializeField]
    public ItemType ItemType { get; set; }
    [field: SerializeField]
    public BodyType BodyType { get; set; }
}
