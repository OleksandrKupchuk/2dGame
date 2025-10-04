using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteracvite {
    private ItemGeneration _itemGeneration = new ItemGeneration();

    [SerializeField]
    private bool _isOpened = false;

    [SerializeField]
    private List<Item> _items;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private Animator _animator;

    public static event Action<List<Item>> OnGetLoot;
    public static event Action OnOpen;

    public void Interact() {
        if (_isOpened) {
            OnOpen?.Invoke();
            _inventory.Open();
            _animator.Play("ChestOpen");
        }
        else {
            OnGetLoot?.Invoke(_itemGeneration.GenerateDropItems(_items));
            OnOpen?.Invoke();
            _inventory.Open();
            _animator.Play("ChestOpen");
            _isOpened = true;
        }
    }
}
