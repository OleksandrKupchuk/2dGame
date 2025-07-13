using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotView : SlotView {
    [SerializeField]
    protected Image _border;
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

    public override void PutItem(ItemData itemData) {
        var _item = itemData as WearableItemData;
        _itemData = _item;
        SetIcon();
        EventManager.OnItemDressedHandler(_item);
    }

    public override bool IsCanPutItem(ItemData itemData) {
        if (itemData == null) return true;

        if (itemData is WearableItemData) {
            var _item = itemData as WearableItemData;
            return _slotTypes.Contains(_item.ItemType);
        }

        return false;
    }

    public override void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(_itemData);
        _itemData = null;
        SetIcon();
    }

    private void ChangeBorderColor(ItemData itemData) {
        if (itemData is WearableItemData) {
            var _item = itemData as WearableItemData;

            if (_slotTypes.Contains(_item.ItemType)) {
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
