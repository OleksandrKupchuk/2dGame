using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {
    private List<Damage> _objectsAttack = new List<Damage>();

    [SerializeField]
    private InvulnerabilityStatus _invulnerabilityStatus;
    [SerializeField]
    private HealthController _healthController;

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
            _healthController.TakeDamage(damageProperties);
        }
    }

    private IEnumerator UnregisteredDamageObject(Damage damageObject) {
        yield return new WaitForSeconds(1f);
        _objectsAttack.Remove(damageObject);
        Debug.Log($"Object {damageObject.name} unregistered");
    }

    private void RegisterDamageObject(Damage damageObject) {
        _objectsAttack.Add(damageObject);
        Debug.Log($"Object {damageObject.name} registered");
        StartCoroutine(UnregisteredDamageObject(damageObject));
    }
}
