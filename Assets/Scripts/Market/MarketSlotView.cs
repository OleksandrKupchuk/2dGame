using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketSlotView : SlotView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private PlayerInputReader _playerInput;
    [SerializeField]
    private Image _borderImage;
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

    public override void PutItem(Item itemData) {
        _itemData = itemData;
        SetIcon();
    }

    public override void RemoveItem() {
        _itemData = null;
        SetIcon();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (IsEmpty) {
                return;
            }

            Debug.Log("BuyItem item");
            _market.BuyItem(_itemData);
            _market.RemoveItem(_itemData);
        }
    }

    public override bool IsCanPutItem(Item itemData) {
        return true;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //print("Inventory slot view ENTER");
        _borderImage.sprite = _activeBorder;
    }

    public void OnPointerExit(PointerEventData eventData) {
        //print("Inventory slot view EXIT");
        ResetBorder();
    }

    private void ResetBorder() {
        //print("Inventory slot view reset border");
        _borderImage.sprite = _defaultBorder;
    }
}
