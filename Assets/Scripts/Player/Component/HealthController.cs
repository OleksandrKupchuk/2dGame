using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthController", menuName = "Controllers/HealthController")]
public class HealthController : ScriptableObject {
    private float _delayBeforeRegenerationHealth;
    private float _timeRegenerationHealth;
    private float _currentHealth;
    private List<Damage> _objectsAttack = new List<Damage>();

    [SerializeField]
    private InvulnerabilityStatus _invulnerabilityStatus;
    [SerializeField]
    private AttributeInteger _healthRegenerationAttribute;
    [SerializeField]
    private AttributeInteger _healthAttribute;
    [SerializeField]
    private PlayerConfig _config;

    public float CurrentHealth { get => _currentHealth; }
    public float MaxHealth { get => _healthAttribute.Value; }
    public bool IsDead { get => CurrentHealth <= 0; }

    private void OnEnable() {
        EventManager.OnHealthChanged += CheckCurrentHealth;
        _currentHealth = _config.Health;
    }

    private void OnDisable() {
        EventManager.OnHealthChanged -= CheckCurrentHealth;
    }

    public void RegenerationHealth() {
        if (_currentHealth >= _healthAttribute.Value || IsDead) {
            _delayBeforeRegenerationHealth = 0;
            return;
        }

        _delayBeforeRegenerationHealth += Time.deltaTime;

        if (_delayBeforeRegenerationHealth >= _config.DelayHealthRegeneration) {

            _timeRegenerationHealth -= Time.deltaTime;

            if (_timeRegenerationHealth <= 0) {
                _timeRegenerationHealth = 1;
                AddHealth(_healthRegenerationAttribute.Value);
                Debug.Log($"regeneration Health + <color=green>{_healthRegenerationAttribute.Value}</color>");
                Debug.Log($"Health after healing + <color=blue>{_healthAttribute.Value}</color>");
            }
        }
    }

    public void AddHealth(float health) {
        _currentHealth += health;

        CheckCurrentHealth();

        Debug.Log("Health was added, value = " + health);
        EventManager.OnHealthChangedHandler();
    }

    private void CheckCurrentHealth() {
        if (_currentHealth >= _healthAttribute.Value) {
            _currentHealth = _healthAttribute.Value;
        }
    }

    public void CheckTakeDamage(List<DamageProperty> damagesProperties, Damage damageObject) {
        if (_objectsAttack.Contains(damageObject)) {
            Task.Delay(System.TimeSpan.FromSeconds(2d)).ContinueWith(task => UnregisteredDamageObject(damageObject));
        }
        else if (_invulnerabilityStatus.IsInvulnerability) {
            Debug.Log("Player IsInvulnerability");
        }
        else {
            RegisterDamageObject(damageObject);
            TakeDamage(damagesProperties);
        }
    }

    private void UnregisteredDamageObject(Damage damageObject) {
        _objectsAttack.Remove(damageObject);
    }

    private void RegisterDamageObject(Damage damageObject) {
        _objectsAttack.Add(damageObject);
    }

    public void TakeDamage(List<DamageProperty> damagesProperties) {
        foreach (var damageProperty in damagesProperties) {
            if (!damageProperty.DamageAttribute.IsEmptyValue()) {
                float _damage = damageProperty.DamageAttribute.Damage - (damageProperty.DamageResistanceAttribute.Value * damageProperty.BlockedDamagePerOneResistance);

                if (_damage <= 0) {
                    return;
                }

                _currentHealth -= _damage;
                damageProperty.DamageViewSpawner.Show(_damage);

                if (IsDead) {
                    EventManager.OnDeadHandler();
                    return;
                }
                else {
                    _delayBeforeRegenerationHealth = 0;
                    EventManager.OnHitHandler();
                    EventManager.OnHealthChangedHandler();
                }
            }
        }
    }
}
