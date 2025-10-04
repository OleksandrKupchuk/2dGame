using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market")]
public class Market : ScriptableObject {

    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Inventory _inventory;

    [field: SerializeField]
    public int AmountSlots { get; private set; }

    public bool IsMarketChecked { get; set; }
    public List<Item> Items { get; private set; }
    public event Action<Item> OnAddItem;
    public event Action<Item> OnRemoveItem;
    public event Action OnOpen;
    public event Action OnClose;
    public int Commission { get; private set; }

    private void OnEnable() {
        IsMarketChecked = false;
    }

    public bool TryBuyItem(Item item) {
        if (_playerConfig.Coins >= item.Price) {
            _playerConfig.Coins -= item.Price;
            item.Price = item.Price - (item.Price * Commission / 100);
            _inventory.TryAddItem(item);
            return true;
        }
        else {
            Debug.LogWarning("Not enough money");
            return false;
        }
    }

    public void SetCommission(int commission) {
        Commission = commission;
    }

    public void Open() {
        OnOpen?.Invoke();
    }

    public void Close() {
        OnClose?.Invoke();
    }

    public void AddItem(Item itemData) {
        OnAddItem?.Invoke(itemData);
    }

    public void RemoveItem(Item itemData) {
        OnRemoveItem?.Invoke(itemData);
    }

    public void SetItems(List<Item> items) {
        Items = items;
    }
}
