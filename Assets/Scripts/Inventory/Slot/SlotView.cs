using UnityEngine;
using UnityEngine.UI;

public abstract class SlotView : MonoBehaviour {
    protected Item _item;

    [SerializeField]
    protected Image _itemIcon;

    public bool IsEmpty => _item == null;
    public Item Item => _item;

    public abstract void PutItem(Item itemData);

    public abstract void RemoveItem();

    public abstract bool IsCanPutItem(Item itemData);

    protected void SetIcon() {
        if (!IsEmpty) {
            _itemIcon.color = new Color(255, 255, 255, 255);
            _itemIcon.sprite = _item.Icon;
        }
        else {
            _itemIcon.color = new Color(255, 255, 255, 0);
        }
    }
}
