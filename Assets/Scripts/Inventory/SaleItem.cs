using UnityEngine;
using UnityEngine.EventSystems;

public class SaleItem : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private InventorySlotView _slotView;
    [SerializeField]
    private SlotZone _slotZone;

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            _market.AddItem(_slotView.ItemData);
            _inventory.RemoveItem(_slotView.ItemData);
            _slotZone.HideItemToolTip();
        }
    }
}
