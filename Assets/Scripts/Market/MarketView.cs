using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private ButtonClosePanels _buttonClosePanels;
    [SerializeField]
    private LayoutElement _layoutElement;

    private void Awake() {
        GenerateCartItems();
        _market.OnOpen += Open;
        _market.OnClose += Close;
        _market.OnAddItem += AddItem;
        _market.OnRemoveItem += RemoveItem;

        _buttonClosePanels.OnClosePanels += Close;

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
                Item _itemData = _creationItem.GetCreatedItem(_market.Items[i]);
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
        _layoutElement.ignoreLayout = false;
        _background.SetActive(true);
    }

    public void Close() {
        _layoutElement.ignoreLayout = true;
        _background.SetActive(false);
    }
}
