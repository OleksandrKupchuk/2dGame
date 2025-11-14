using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration {
    public List<Item> GenerateDropItems(List<Item> items) {
        List<Item> _spawnedItems = new List<Item>();

        foreach (var item in items) {
            float _itemSpawnChance = Random.Range(0, 100);
            Debug.Log($"Chance spawn item {item.Name} = " + _itemSpawnChance);

            if (_itemSpawnChance <= item.SpawnChance) {
                _spawnedItems.Add(item);
            }
        }

        return _spawnedItems;
    }
}
