using UnityEngine;
using UnityEngine.EventSystems;

public class BuyItem : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private MarketSlotView _slotView;
    [SerializeField]
    private SlotZone _slotZone;

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (_market.TryBuyItem(_slotView.Item)) {
                _slotView.RemoveItem();
                _slotZone.HideItemToolTip();
            }
        }
    }
}
