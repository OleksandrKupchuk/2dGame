using UnityEngine;
using UnityEngine.EventSystems;

public class ChestSlotView : SlotView, IPointerClickHandler {
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private ItemToolTip _itemToolTip;

    private void Awake() {
        DragAndDrop.OnDragStarted += ResetBorder;
    }

    private void OnDestroy() {
        DragAndDrop.OnDragStarted -= ResetBorder;
    }

    public override bool IsCanPutItem(Item itemData) {
        return true;
    }

    public override void PutItem(Item itemData) {
        _item = itemData;
        SetIcon();
    }

    public override void RemoveItem() {
        _item = null;
        SetIcon();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (IsEmpty) {
                return;
            }

            if(!_inventory.CanAddItem) {
                Debug.Log("In Inventory there is not a place");
                return;
            }

            Debug.Log("Right click in chest");
            _inventory.AddItem(_item);
            RemoveItem();
            _itemToolTip.Hide();
        }
    }
}
