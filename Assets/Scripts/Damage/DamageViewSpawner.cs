using UnityEngine;

[CreateAssetMenu(fileName = "DamageViewSpawner", menuName = "Damage/DamageViewSpawner")]
public class DamageViewSpawner : ScriptableObject {
    private Canvas _parent;
    private ObjectPool<DamageView> _poolDamageView;

    [SerializeField]
    private DamageView _prefab;

    public void SpawnDamageView(float damage, Color color, Vector2 spawnPosition, float startScale, float endScale) {
        if (_parent == null) {
            _parent = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        }

        if(_poolDamageView == null) {
            _poolDamageView = new ObjectPool<DamageView>(_prefab, _parent.transform);
        }

        DamageView _damageView = _poolDamageView.GetEnabledObject();
        _damageView.transform.position = new Vector2(spawnPosition.x, spawnPosition.y + 4f);
        _damageView.SetScale(startScale, endScale);
        _damageView.ShowDamage(damage, color);
    }
}
