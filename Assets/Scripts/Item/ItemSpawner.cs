using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    private RaycastHit2D _raycastHit;

    [SerializeField]
    private ItemView _prefab;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private float _spawnDistance;
    [SerializeField]
    private LayerMask _layersTrigger;

    private void Awake() {
        EventManager.OnItemDrop += SpawnItem;
    }

    private void OnDestroy() {
        EventManager.OnItemDrop -= SpawnItem;
    }

    private void Update() {
        print("can spawn item = " + IsCanSpawnItem());
    }

    public bool IsCanSpawnItem() {
        Color _color;
        _raycastHit = Physics2D.Raycast(_boxCollider.bounds.center, Vector2.left, _spawnDistance * transform.localScale.x, _layersTrigger);

        _color = _raycastHit.transform == null ? Color.green : Color.red;

        Debug.DrawRay(_boxCollider.bounds.center, Vector2.left * _spawnDistance * transform.localScale.x, _color);

        if (_raycastHit.transform != null) {
            print("transform = " + _raycastHit.transform);
        }

        return _raycastHit.transform == null;
    }

    public void SpawnItem(Item item) {
        if (IsCanSpawnItem()) {
            ItemView _itemView = Instantiate(_prefab, new Vector3(transform.position.x - (_spawnDistance * transform.localScale.x), transform.position.y + 1f, 0), Quaternion.identity);
            _itemView.SetItem(item);
            _itemView.SetIcon();

            _inventory.RemoveItem(item);
        }
        else {
            Debug.Log("Can`t drop item");
        }
    }
}
