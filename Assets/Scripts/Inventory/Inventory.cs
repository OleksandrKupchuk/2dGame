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

    public bool IsEmptySlot => AmountSlots > _items.Count;
    public List<Item> Items => _items;

    private void OnEnable() {
        _items.Clear();
    }

    public bool TryAddItem(Item itemData) {
        if(!IsEmptySlot) {
            Debug.Log("In Inventory there is not a place");
            return false;
        }
        //if(_items.Contains(itemData)) {
        //    Debug.Log("Item already add");
        //    return false;
        //}

        _items.Add(itemData);
        OnItemAdd?.Invoke(itemData);

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
