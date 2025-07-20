using UnityEngine;

[CreateAssetMenu]
public class ItemToolTip : ScriptableObject {
    private ItemToolTipView _itemToolTipView;
    private Canvas _parent;

    [SerializeField]
    private ItemToolTipView _prefab;

    public void Show(ItemData itemData, RectTransform rectTransform) {
        if (_itemToolTipView == null) {
            _parent = FindObjectOfType<Canvas>();
            _itemToolTipView = Instantiate(_prefab, _parent.transform);
        }

        _itemToolTipView.Enable(itemData, rectTransform, 0);
    }

    public void Hide() {
        _itemToolTipView.DisableBackground();
    }
}
