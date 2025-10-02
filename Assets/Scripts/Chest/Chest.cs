using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour, IInteracvite {
    [SerializeField]
    private bool _isOpened = false;

    [SerializeField]
    private List<Item> _items;
    [SerializeField]
    private Inventory _inventory;

    public static event Action<List<Item>> OnGetLoot;
    public static event Action OnOpen;

    public void Interact() {
        if (_isOpened) {
            OnOpen?.Invoke();
            _inventory.Open();
        }
        else {
            OnGetLoot?.Invoke(GetItems());
            OnOpen?.Invoke();
            _inventory.Open();
            _isOpened = true;
        }
    }

    private List<Item> GetItems() {
        List<Item> _spawnedItems = new List<Item>();

        foreach (var item in _items) {
            float _itemSpawnChance = Random.Range(0, 100);
            Debug.Log($"Chance spawn item {item.Name} = " + _itemSpawnChance);

            if (_itemSpawnChance <= item.SpawnChance) {
                _spawnedItems.Add(item);
            }
        }

        return _spawnedItems;
    }
}
