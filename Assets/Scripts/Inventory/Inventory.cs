using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {
    [SerializeField]
    [Unity.Collections.ReadOnly]
    private List<Item> _items = new List<Item>();

    [field: SerializeField]
    public int AmountSlots { get; private set; }

    public event Action OnOpen;
    public event Action OnClose;
    public event Action<Item> OnItemAdd;
    public event Action<Item> OnItemRemove;

    public bool CanAddItem => AmountSlots > _items.Count;
    public List<Item> Items => _items;

    private void OnEnable() {
        _items.Clear();
    }

    public void AddItem(Item item) {
        _items.Add(item);
        OnItemAdd?.Invoke(item);
    }

    public bool TryAddItem(Item item) {
        if(!CanAddItem) {
            Debug.Log("In Inventory there is not a place");
            return false;
        }

        _items.Add(item);
        OnItemAdd?.Invoke(item);
        return true;
    }

    public void RemoveItem(Item itemData) {
        if(!_items.Contains(itemData)) {
            Debug.Log("Can`t remove item because it not was added");
            return;
        }

        _items.Remove(itemData);
        OnItemRemove?.Invoke(itemData);
    }

    public void Open() {
        OnOpen?.Invoke();
    }

    public void Close() {
        OnClose?.Invoke();
    }
}
