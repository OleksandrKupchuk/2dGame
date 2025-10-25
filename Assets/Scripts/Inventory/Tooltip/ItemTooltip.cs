using UnityEngine;

[CreateAssetMenu(fileName = "ItemToolTip", menuName = "Item/ItemToolTip")]
public class ItemToolTip : ScriptableObject {
    private ItemToolTipView _itemToolTipView;
    private Canvas _parent;

    [SerializeField]
    private ItemToolTipView _prefab;

    public bool IsActive => _itemToolTipView != null && _itemToolTipView.IsActive;

    public void Show(Item item, RectTransform rectTransform) {
        if (_itemToolTipView == null) {
            _parent = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>();
            _itemToolTipView = Instantiate(_prefab, _parent.transform);
        }

        if(item == null) {
            Debug.LogWarning("ItemToolTip: item is null");
            return;
        }

        _itemToolTipView.Enable(item, rectTransform);
    }

    public void Hide() {
        _itemToolTipView.DisableBackground();
    }
}
