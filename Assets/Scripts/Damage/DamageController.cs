using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    private List<Damage> _objectsAttack = new List<Damage>();

    [SerializeField]
    private InvulnerabilityStatus _invulnerabilityStatus;
    [SerializeField]
    private HealthController _healthController;
    [SerializeField]
    private float _invulnerabilityTime;
    [SerializeField]
    private DamageViewSpawner _damageViewSpawner;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Damage damage)) {
            TryTakeDamage(damage.Attributes.DamageAttributeProperties, damage);
        }
    }

    public void TryTakeDamage(List<DamageAttributeProperty> damageProperties, Damage damageObject) {
        if (_objectsAttack.Contains(damageObject)) {
            Debug.Log($"Object {damageObject.name} already give damage");
        }
        else if (_invulnerabilityStatus.IsInvulnerability) {
            Debug.Log("Object IsInvulnerability " + gameObject.name);
        }
        else {
            RegisterDamageObject(damageObject);

            foreach (var damageProperty in damageProperties) {
                float _damage = damageProperty.DamageAttribute.Damage - (damageProperty.ResistanceAttribute.Value * damageProperty.BlockedDamagePerOneResistance);

                if (_damage <= 0) {
                    return;
                }

                _healthController.TakeDamage(_damage);
                TryCallDurationDamage(damageProperty, _damage);
                _damageViewSpawner.SpawnDamageView(_damage, damageProperty.Color);
            }
        }
    }

    private void TryCallDurationDamage(DamageAttributeProperty damageProperty, float baseDamage) {
        if (damageProperty.IsDealDurationDamage) {
            Debug.Log($"Duration damage already deal");
            return;
        }

        float _durationDamage = damageProperty.DurationDamage.PercentFromBaseDamage * baseDamage/ 100;

        StartCoroutine(damageProperty.DealDurationDamage(_healthController, _durationDamage));
    }

    private IEnumerator UnregisteredDamageObject(Damage damageObject) {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _objectsAttack.Remove(damageObject);
        Debug.Log($"Object {damageObject.name} unregistered");
    }

    private void RegisterDamageObject(Damage damageObject) {
        _objectsAttack.Add(damageObject);
        Debug.Log($"Object {damageObject.name} registered");
        StartCoroutine(UnregisteredDamageObject(damageObject));
    }
}
