using UnityEngine;

public class DamageViewSpawner : MonoBehaviour {
    private ObjectPool<DamageView> _poolDamageView;

    [SerializeField]
    private DamageView _prefab;
    [SerializeField]
    private Transform _parent;
    [SerializeField]
    private Transform _spawnPoint;

    private void Awake() {
        _poolDamageView = new ObjectPool<DamageView>(_prefab, _parent.transform);
    }

    public void SpawnDamageView(float damage, Color color, float startScale, float endScale) {
        DamageView _damageView = _poolDamageView.GetEnabledObject();
        _damageView.transform.position = _spawnPoint.position;
        _damageView.SetScale(startScale, endScale);
        _damageView.ShowDamage(damage, color);
    }
}
