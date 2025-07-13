using UnityEngine;
using UnityEngine.EventSystems;

public class SlotZone : MonoBehaviour, IDropHandler {
    [SerializeField]
    private SlotView _slotView;

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
            ItemData _itemData = dragAndDrop.SlotView.ItemData;

            if (_slotView.IsCanPutItem(_itemData)) {
                dragAndDrop.SlotView.RemoveItem();
                dragAndDrop.SlotView.PutItem(_slotView.ItemData);
                _slotView.RemoveItem();
                _slotView.PutItem(_itemData);
            }
        }
    }
}
