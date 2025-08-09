using UnityEngine;

[CreateAssetMenu]
public class ItemToolTip : ScriptableObject {
    private ItemToolTipView _itemToolTipView;
    private Canvas _parent;

    [SerializeField]
    private ItemToolTipView _prefab;

    public bool IsActive => _itemToolTipView != null && _itemToolTipView.IsActive;

    public void Show(ItemData itemData, RectTransform rectTransform) {
        if (_itemToolTipView == null) {
            _parent = FindObjectOfType<Canvas>();
            _itemToolTipView = Instantiate(_prefab, _parent.transform);
        }

        _itemToolTipView.Enable(itemData, rectTransform);
    }

    public void Hide() {
        _itemToolTipView.DisableBackground();
    }
}
