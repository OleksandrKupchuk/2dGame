using UnityEngine;

[CreateAssetMenu(fileName = "DurationDamage", menuName = "Damage/DurationDamage")]
public class DurationDamage : ScriptableObject {
    [field: SerializeField]
    public float Duration { get; private set; }
    [field: SerializeField, Range(0, 25)]
    public float DamageFrequency { get; private set; }
    [field: SerializeField]
    public float PercentFromBaseDamage { get; private set; }
    //[field: SerializeField]
    //public DamageViewSpawner DamageViewSpawner { get; private set; }
}
