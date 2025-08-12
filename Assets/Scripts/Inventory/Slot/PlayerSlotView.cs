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

    public override void PutItem(Item itemData) {
        var _itemData = itemData as WearableItem;
        base._itemData = _itemData;
        SetIcon();
        EventManager.OnItemDressedHandler(_itemData);
    }

    public override bool IsCanPutItem(Item itemData) {
        if (itemData == null) return true;

        if (itemData is WearableItem) {
            var _itemData = itemData as WearableItem;
            return _slotTypes.Contains(_itemData.ItemTypeAttribute);
        }

        return false;
    }

    public override void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(_itemData);
        _itemData = null;
        SetIcon();
    }

    private void ChangeBorderColor(Item itemData) {
        if (itemData is WearableItem) {
            var _item = itemData as WearableItem;

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
