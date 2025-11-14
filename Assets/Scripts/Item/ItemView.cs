using UnityEngine;

public class ItemView : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer _icon;

    [field: SerializeField]
    public Item ItemData { get; private set; }

    private void OnEnable() {
        gameObject.layer = LayerMask.NameToLayer("Item");
    }

    public void SetItem(Item item) {
        ItemData = item;
    }

    public void SetIcon() {
        _icon.sprite = ItemData.Icon;
    }
}
