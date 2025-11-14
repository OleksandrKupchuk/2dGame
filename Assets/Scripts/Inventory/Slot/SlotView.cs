using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    protected Item _item;

    [SerializeField]
    protected Image _itemIcon;
    [SerializeField]
    protected Image _border;
    [SerializeField]
    protected Sprite _defaultBorder;
    [SerializeField]
    protected Sprite _activeBorder;

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

    public void OnPointerEnter(PointerEventData eventData) {
        //print("Inventory slot view ENTER");
        _border.sprite = _activeBorder;
    }

    public void OnPointerExit(PointerEventData eventData) {
        //print("Inventory slot view EXIT");
        ResetBorder();
    }

    protected void ResetBorder() {
        //print("Inventory slot view reset border");
        _border.sprite = _defaultBorder;
    }
}
