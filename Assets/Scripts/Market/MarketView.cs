using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketView : MonoBehaviour {
    private List<MarketSlotView> _slots = new List<MarketSlotView>();
    private CreationItem _creationItem = new CreationItem();

    [SerializeField]
    private int _amountSlots;
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

    public static bool IsOpen { get; private set; }

    private void Awake() {
        GenerateSlots();
        _market.OnOpen += Open;
        _market.OnClose += Close;
        _market.OnAddItem += AddItem;
        _market.OnRemoveItem += RemoveItem;
        _buttonClosePanels.OnClosePanels += Close;

        Close();
    }

    private void GenerateSlots() {
        for (int i = 0; i < _amountSlots; i++) {
            MarketSlotView _slotViewObject = Instantiate(_slotView, _content);
            _slotViewObject.PutItem(null);
            _slots.Add(_slotViewObject);
        }
    }

    private void OnDestroy() {
        _market.OnOpen -= Open;
        _market.OnClose -= Close;
        _market.OnAddItem -= AddItem;
        _market.OnRemoveItem -= RemoveItem;
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
        if (!_market.IsMarketChecked) {
            StartCoroutine(MarketRestock.Instance.StartUpdatingItems());
            PutItems();
            _market.IsMarketChecked = true;
        }

        _layoutElement.ignoreLayout = false;
        _background.SetActive(true);
        IsOpen = true;
    }

    private void PutItems() {
        for (int i = 0; i < _market.AmountSlots; i++) {
            if (i < _market.Items.Count) {
                Item _item = _creationItem.GetCreatedItem(_market.Items[i]);
                int _newPrice = _item.Price + (_item.Price * _market.Commission / 100);
                _item.Price = _newPrice;
                _slots[i].PutItem(_item);
            }
        }
    }

    public void Close() {
        _layoutElement.ignoreLayout = true;
        _background.SetActive(false);
        IsOpen = false;
    }
}
