using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market")]
public class Market : ScriptableObject {
    private int _commission;

    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private List<Item> _items;

    [field: SerializeField]
    public int AmountSlots { get; private set; }

    public List<Item> Items => _items;
    public event Action<Item> OnAddItem;
    public event Action<Item> OnRemoveItem;
    public event Action OnOpen;
    public event Action OnClose;

    public void BuyItem(Item item) {
        int _itemPrice = GetPriceWithTraderCommission(item.Price);

        if (_playerConfig.Coins >= _itemPrice) {
            _playerConfig.Coins -= _itemPrice;
            _inventory.TryAddItem(item);
        }
        else {
            Debug.LogWarning("Not enough money");
        }
    }

    private int GetPriceWithTraderCommission(int itemPrice) {
        return itemPrice + (itemPrice * _commission / 100);
    }

    public void SetCommission(int commission) {
        _commission = commission;
    }

    public void Open() {
        OnOpen?.Invoke();
    }

    public void Close() {
        OnClose?.Invoke();
    }

    public void AddItem(Item itemData) {
        _items.Add(itemData);
        OnAddItem?.Invoke(itemData);
    }

    public void RemoveItem(Item itemData) {
        _items.Remove(itemData);
        OnRemoveItem?.Invoke(itemData);
    }
}
