using UnityEngine;

[CreateAssetMenu(fileName = "DamageProperty", menuName = "Attribute/DamageProperty")]
public class DamageProperty : ScriptableObject {
    [field: SerializeField]
    public AttributeRange DamageAttribute { get; private set; }
    [field: SerializeField]
    public AttributeInteger DamageResistanceAttribute { get; private set; }
    [field: SerializeField]
    public float BlockedDamagePerOneResistance { get; private set; }
    [field: SerializeField]
    public float DamageImmunity { get; private set; }
    [field: SerializeField]
    public DamageViewSpawner DamageViewSpawner { get; private set; }
}
