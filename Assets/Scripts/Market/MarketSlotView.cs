using UnityEngine;

public class MarketSlotView : SlotView {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private PlayerInputReader _playerInput;
    [SerializeField]
    private ItemToolTip _itemToolTip;

    private void Awake() {
        DragAndDrop.OnDragStarted += ResetBorder;
    }

    private void OnDestroy() {
        DragAndDrop.OnDragStarted -= ResetBorder;
    }

    public override void PutItem(Item itemData) {
        _item = itemData;
        SetIcon();
    }

    public override void RemoveItem() {
        _item = null;
        SetIcon();
    }

    public override bool IsCanPutItem(Item itemData) {
        return true;
    }
}
