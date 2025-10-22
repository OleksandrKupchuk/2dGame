using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthController", menuName = "Components/HealthController")]
public class HealthController : ScriptableObject {
    private float _delayBeforeRegenerationHealth;
    private float _timeRegenerationHealth;
    private float _currentHealth;

    [SerializeField]
    private float _delayHealthRegenerationInSeconds;
    [SerializeField]
    private CharacterFixedAttribute _healthRegenerationAttribute;
    [SerializeField]
    private CharacterFixedAttribute _healthAttribute;
    [SerializeField]
    private CharacterAttributesConfig _config;

    public float CurrentHealth { get => _currentHealth; }
    public float MaxHealth { get => _healthAttribute.Value; }
    public bool IsDead { get => CurrentHealth <= 0; }

    public event Action OnHit;
    public event Action OnDead;
    public event Action OnHealthChanged;

    private void OnEnable() {
        OnHealthChanged += CheckCurrentHealth;
        _currentHealth = _config.Health;
    }

    private void OnDisable() {
        OnHealthChanged -= CheckCurrentHealth;
    }

    public void RegenerationHealth() {
        if (IsDead) {
            return;
        }

        if (_currentHealth >= _healthAttribute.Value) {
            _delayBeforeRegenerationHealth = 0;
            return;
        }

        _delayBeforeRegenerationHealth += Time.deltaTime;

        if (_delayBeforeRegenerationHealth >= _delayHealthRegenerationInSeconds) {

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
        OnHealthChanged.Invoke();
    }

    private void CheckCurrentHealth() {
        if (_currentHealth >= _healthAttribute.Value) {
            _currentHealth = _healthAttribute.Value;
        }
    }

    public void TakeDamage(float damage) {
        _currentHealth -= damage;
        OnHealthChanged.Invoke();

        if (IsDead) {
            OnDead.Invoke();
            return;
        }

        _delayBeforeRegenerationHealth = 0;
        OnHit?.Invoke();
    }
}
