using UnityEngine;

public class ItemView : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer _icon;

    [field: SerializeField]
    public Item ItemData { get; private set; }

    private void OnEnable() {
        //SetIcon();
    }

    public void SetItemData(Item itemData) {
        ItemData = itemData;
    }

    public void SetIcon() {
        _icon.sprite = ItemData.Icon;
    }
}
