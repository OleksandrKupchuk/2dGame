using UnityEngine;

[CreateAssetMenu(fileName = "UsableItemData", menuName = "ItemData/UsableItemData")]
public class UsableItemData : ItemData {
    [field: SerializeField]
    public float Duration { get; protected set; }

    public void Use() {
        EventManager.OnItemDressedHandler(this);
    }
}
