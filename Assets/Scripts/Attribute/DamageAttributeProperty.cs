using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageAttributeProperty", menuName = "Attributes/DamageAttributeProperty")]
public class DamageAttributeProperty : ScriptableObject {
    [SerializeField]
    private bool _isDealDurationDamage;

    [field: SerializeField]
    public AttributeRange DamageAttribute { get; private set; }
    [field: SerializeField]
    public AttributeInteger ResistanceAttribute { get; private set; }
    [field: SerializeField]
    public float BlockedDamagePerOneResistance { get; private set; }
    [field: SerializeField]
    public float Immunity { get; private set; }
    [field: SerializeField]
    public Color Color { get; private set; }
    [field: SerializeField, Range(0, 100)]
    public float ChanceApplyDurationDamage { get; private set; }
    [field: SerializeField]
    public DurationDamage DurationDamage { get; private set; }
    public bool IsDealDurationDamage => _isDealDurationDamage;

    public IEnumerator DealDurationDamage(HealthController healthController, DamageViewSpawner damageViewSpawner) {
        _isDealDurationDamage = true;
        int _amountDamageReceiver = (int)(DurationDamage.Duration / DurationDamage.DamageFrequency);

        for (int i = 0; i < _amountDamageReceiver; i++) {
            if (!healthController.IsDead) {
                yield return new WaitForSeconds(DurationDamage.DamageFrequency);
                float _damage = DurationDamage.PercentFromBaseDamage * DamageAttribute.Damage / 100;
                Debug.Log("Take Duration damage = " + _damage);
                healthController.TakeDamage(_damage);
                damageViewSpawner.SpawnDamageView(_damage, Color, 0.4f, 0.7f);
            }
        }

        _isDealDurationDamage = false;
        yield return null;
    }
}
