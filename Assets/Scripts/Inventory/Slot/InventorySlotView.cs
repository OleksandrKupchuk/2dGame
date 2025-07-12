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
    [SerializeField]
    private DragAndDrop _dragAndDrop;

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

    public override void TakeItem() {
        _itemData = null;
        SetIcon();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        _border.sprite = _activeBorder;
    }

    public void OnPointerExit(PointerEventData eventData) {
        ResetBorder();
    }

    private void ResetBorder() {
        _border.sprite = _defaultBorder;
    }
}
