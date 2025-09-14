using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthController", menuName = "Components/HealthController")]
public class HealthController : ScriptableObject {
    private float _delayBeforeRegenerationHealth;
    private float _timeRegenerationHealth;
    private float _currentHealth;

    [SerializeField]
    private float _delayHealthRegenerationInSeconds;
    [SerializeField]
    private AttributeInteger _healthRegenerationAttribute;
    [SerializeField]
    private AttributeInteger _healthAttribute;
    [SerializeField]
    private AttributesConfig _config;

    public float CurrentHealth { get => _currentHealth; }
    public float MaxHealth { get => _healthAttribute.Value; }
    public bool IsDead { get => CurrentHealth <= 0; }

    public event Action<float, Color> OnTakeDamage;
    public event Action OnHit;
    public event Action OnDead;

    private void OnEnable() {
        EventManager.OnHealthChanged += CheckCurrentHealth;
        _currentHealth = _config.Health;
    }

    private void OnDisable() {
        EventManager.OnHealthChanged -= CheckCurrentHealth;
    }

    public void RegenerationHealth() {
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
        EventManager.OnHealthChangedHandler();
    }

    private void CheckCurrentHealth() {
        if (_currentHealth >= _healthAttribute.Value) {
            _currentHealth = _healthAttribute.Value;
        }
    }

    public void TakeDamage(List<DamageAttributeProperty> damageProperties) {
        foreach (var damageProperty in damageProperties) {
            float _damage = damageProperty.DamageAttribute.Damage - (damageProperty.ResistanceAttribute.Value * damageProperty.BlockedDamagePerOneResistance);

            if (_damage <= 0) {
                return;
            }

            if (IsDead) {
                OnDead.Invoke();
                return;
            }
            else {
                _currentHealth -= _damage;
                OnTakeDamage.Invoke(_damage, damageProperty.Color);
                _delayBeforeRegenerationHealth = 0;
                OnHit?.Invoke();
                EventManager.OnHealthChangedHandler();
            }
        }
    }

    public void TakeDamage(float damage) {
        if (IsDead) {
            OnDead.Invoke();
            //return;
        }
        else {
            _currentHealth -= damage;
            _delayBeforeRegenerationHealth = 0;
            OnHit?.Invoke();
            EventManager.OnHealthChangedHandler();
        }
    }
}
