using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotView : SlotView {
    [SerializeField]
    protected Image _border;
    [SerializeField]
    private List<ItemTypeAttribute> _slotTypes = new List<ItemTypeAttribute>();

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

        if (item.ItemType.Equals(ItemType.Wearable)) {
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
        if (item.ItemType.Equals(ItemType.Wearable)) {
            if (_slotTypes.Contains(_item.ItemTypeAttribute)) {
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
