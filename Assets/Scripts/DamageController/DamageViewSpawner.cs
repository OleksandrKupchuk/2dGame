using UnityEngine;

[CreateAssetMenu(fileName = "DamageViewSpawner", menuName = "Controllers/DamageViewSpawner")]
public class DamageViewSpawner : ScriptableObject {
    private Canvas _canvas;
    private ObjectPool<DamageView> _poolDamageView;

    [SerializeField]
    private DamageView _damageViewPrefab;

    public void Show(float damage) {
        if (_canvas == null) {
            _canvas = FindObjectOfType<Canvas>();
        }

        if (_poolDamageView == null) {
            _poolDamageView = new ObjectPool<DamageView>(_damageViewPrefab, _canvas.transform);
        }

        DamageView _damageView = _poolDamageView.GetDisabled();
        _damageView.Enable(damage);
    }
}
