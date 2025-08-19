using UnityEngine;
using UnityEngine.EventSystems;

public class MarketSlotView : SlotView, IPointerClickHandler {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private PlayerInputReader _playerInput;

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

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (IsEmpty) {
                return;
            }

            Debug.Log("BuyItem item");
            _market.BuyItem(_item);
            _market.RemoveItem(_item);
        }
    }

    public override bool IsCanPutItem(Item itemData) {
        return true;
    }
}
