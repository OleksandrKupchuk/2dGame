using System.Collections.Generic;
using UnityEngine;

public class PlayerSlotView : SlotView {
    [SerializeField]
    private List<ItemType> _slotTypes = new List<ItemType>();

    private void Awake() {
        SetIcon();
        DragAndDrop.OnItemDragged += ChangeBorderColor;
        DragAndDrop.OnDragEnded += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemDragged -= ChangeBorderColor;
        DragAndDrop.OnDragEnded -= ResetBorderColor;
    }

    public override void PutItem(Item item) {
        _item = item;
        SetIcon();
        EventManager.OnItemDressedHandler(_item);
    }

    public override bool IsCanPutItem(Item item) {
        if (item == null) return true;

        if (item.ItemCategory.Equals(ItemCategory.Wearable)) {
            return _slotTypes.Contains(item.ItemTypeAttribute);
        }

        return false;
    }

    public override void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(_item);
        _item = null;
        SetIcon();
    }

    private void ChangeBorderColor(Item item) {
        if (item.ItemCategory.Equals(ItemCategory.Wearable)) {
            if (_slotTypes.Contains(item.ItemTypeAttribute)) {
                SetBorderColor(Color.green);
            }
            else {
                SetBorderColor(Color.red);
            }
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }
}
