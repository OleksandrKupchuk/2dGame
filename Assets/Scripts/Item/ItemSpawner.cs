using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [SerializeField]
    private ItemView _prefab;
    [SerializeField]
    private Inventory _inventory;

    private void Awake() {
        //_inventory.OnRemoveItem += SpawnItem;
    }

    private void OnDestroy() {
        //_inventory.OnRemoveItem -= SpawnItem;
    }

    public void SpawnItem(Item itemData) {
        ItemView _itemView = Instantiate(_prefab, new Vector3(0, 0, 10), Quaternion.identity);
        _itemView.SetItemData(itemData);
        _itemView.SetIcon();
    }
}
