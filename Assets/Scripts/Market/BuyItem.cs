using UnityEngine;
using UnityEngine.EventSystems;

public class BuyItem : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private MarketSlotView _slotView;
    [SerializeField]
    private SlotZone _slotZone;

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            _inventory.TryAddItem(_slotView.ItemData);
            _market.RemoveItem(_slotView.ItemData);
            _slotZone.HideItemToolTip();
        }
    }
}
