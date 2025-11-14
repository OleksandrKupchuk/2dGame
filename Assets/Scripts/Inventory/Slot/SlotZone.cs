using UnityEngine;
using UnityEngine.EventSystems;

public class SlotZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    private bool _canShowToolTip = true;

    [SerializeField]
    private SlotView _slotView;
    [SerializeField]
    private ItemToolTip _itemToolTip;
    [SerializeField]
    private RectTransform _rectTransform;

    private void Awake() {
        DragAndDrop.OnDragStarted += HideItemToolTip;
        DragAndDrop.OnDragStarted += ForbidShowTooTip;
        DragAndDrop.OnDragEnded += AllowShowTooTip;
    }

    private void OnDestroy() {
        DragAndDrop.OnDragStarted -= HideItemToolTip;
        DragAndDrop.OnDragStarted -= ForbidShowTooTip;
        DragAndDrop.OnDragEnded -= AllowShowTooTip;
    }

    private void ForbidShowTooTip() {
        _canShowToolTip = false;
    }

    private void AllowShowTooTip() {
        _canShowToolTip = true;
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
            Item _item = dragAndDrop.SlotView.Item;

            if (_slotView.IsCanPutItem(_item)) {
                dragAndDrop.SlotView.RemoveItem();
                dragAndDrop.SlotView.PutItem(_slotView.Item);
                _slotView.RemoveItem();
                _slotView.PutItem(_item);
                _itemToolTip.Show(_slotView.Item, _rectTransform);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (_slotView.IsEmpty) {
            return;
        }

        if (_canShowToolTip) {
            _itemToolTip.Show(_slotView.Item, _rectTransform);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_slotView.IsEmpty) {
            return;
        }

        //print("Exit slot zone");
        HideItemToolTip();
    }

    public void HideItemToolTip() {
        _itemToolTip.Hide();
    }
}
