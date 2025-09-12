using UnityEngine;

public class DamageViewSpawner : MonoBehaviour {
    private Canvas _parent;
    private ObjectPool<DamageView> _poolDamageView;

    [SerializeField]
    private DamageView _prefab;
    [SerializeField]
    private HealthController _healthController;

    private void Awake() {
        _parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        //_healthController.OnTakeDamage += SpawnDamageView;
        _poolDamageView = new ObjectPool<DamageView>(_prefab, _parent.transform);
    }

    private void OnDestroy() {
        //_healthController.OnTakeDamage -= SpawnDamageView;
    }

    public void SpawnDamageView(float damage, Color color) {
        DamageView _damageView = _poolDamageView.GetEnabledObject();
        _damageView.transform.position = new Vector2(transform.position.x, transform.position.y + 4f);
        _damageView.ShowDamage(damage, color);
    }
}
