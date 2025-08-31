using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {
    private List<InventorySlotView> _slots = new List<InventorySlotView>();

    [SerializeField]
    private Market _market;
    [SerializeField]
    private InventorySlotView _slotViewPrefab;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private Button _closeButton;

    private void Awake() {
        SpawnSlotsView();
        _inventory.OnOpen += Open;
        _inventory.OnClose += Close;
        _inventory.OnItemAdd += AddItem;
        _inventory.OnItemRemove += RemoveItem;
        _closeButton.onClick.AddListener(() => { Close(); _market.Close(); });

        Close();
    }

    private void OnDestroy() {
        _inventory.OnOpen -= Open;
        _inventory.OnClose -= Close;
        _inventory.OnItemAdd -= AddItem;
        _inventory.OnItemRemove -= RemoveItem;
    }

    private void SpawnSlotsView() {
        for (int i = 0; i < _inventory.AmountSlots; i++) {
            InventorySlotView _slot = Instantiate(_slotViewPrefab, _bag);
            _slot.gameObject.name = _slot.gameObject.name + " " + i;
            _slot.PutItem(null);
            _slots.Add(_slot);
        }
    }

    private void AddItem(Item itemData) {
        foreach (var _slot in _slots) {
            if(_slot.IsEmpty) {
                _slot.PutItem(itemData);
                return;
            }
        }
    }

    private void RemoveItem(Item itemData) {
        foreach (var _slot in _slots) {
            if (!_slot.IsEmpty && _slot.Item == itemData) {
                _slot.RemoveItem();
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
