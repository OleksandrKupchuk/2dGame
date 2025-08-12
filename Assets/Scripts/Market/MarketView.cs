using System.Collections.Generic;
using UnityEngine;

public class MarketView : MonoBehaviour {
    private List<MarketSlotView> _slots = new List<MarketSlotView>();
    private CreationItem _creationItem = new CreationItem();

    [SerializeField]
    private Market _market;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private MarketSlotView _slotView;
    [SerializeField]
    private Transform _content;

    private void Awake() {
        GenerateCartItems();
        _market.OnOpen += Open;
        _market.OnClose += Close;
        _market.OnAddItem += AddItem;
        _market.OnRemoveItem += RemoveItem;

        Close();
    }

    private void OnDestroy() {
        _market.OnOpen -= Open;
        _market.OnClose -= Close;
        _market.OnAddItem -= AddItem;
        _market.OnRemoveItem -= RemoveItem;
    }

    private void GenerateCartItems() {
        for (int i = 0; i < _market.AmountSlots; i++) {
            MarketSlotView _slotViewObject = Instantiate(_slotView, _content);

            if (i < _market.Items.Count) {
                Item _itemData = _creationItem.CreateWearableItemData(_market.Items[i]);
                _slotViewObject.PutItem(_itemData);
            }
            else {
                _slotViewObject.PutItem(null);
            }

            _slots.Add(_slotViewObject);
        }
    }

    private void AddItem(Item itemData) {
        foreach (var slot in _slots) {
            if (slot.IsEmpty) {
                slot.PutItem(itemData);
                return;
            }
        }
    }

    private void RemoveItem(Item itemData) {
        foreach (var slot in _slots) {
            if (slot.Item == itemData) {
                slot.RemoveItem();
                return;
            }
        }
    }

    public void Open() {
        _background.SetActive(true);
    }

    public void Close() {
        _background.SetActive(false);
    }
}
