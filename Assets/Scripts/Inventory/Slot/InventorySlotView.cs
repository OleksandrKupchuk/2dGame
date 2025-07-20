using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotView : SlotView, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private Image _border;
    [SerializeField]
    private Sprite _defaultBorder;
    [SerializeField]
    private Sprite _activeBorder;

    private void Awake() {
        DragAndDrop.OnDragStarted += ResetBorder;
    }

    private void OnDestroy() {
        DragAndDrop.OnDragStarted -= ResetBorder;
    }

    public override void PutItem(ItemData itemData) {
        _itemData = itemData;
        SetIcon();
    }

    public override void RemoveItem() {
        _itemData = null;
        SetIcon();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //print("Inventory slot view ENTER");
        _border.sprite = _activeBorder;
    }

    public void OnPointerExit(PointerEventData eventData) {
        //print("Inventory slot view EXIT");
        ResetBorder();
    }

    private void ResetBorder() {
        //print("Inventory slot view reset border");
        _border.sprite = _defaultBorder;
    }

    public override bool IsCanPutItem(ItemData itemData) {
        return true;
    }
}
