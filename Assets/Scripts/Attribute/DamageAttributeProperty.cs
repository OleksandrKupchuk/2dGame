using UnityEngine;

[CreateAssetMenu(fileName = "DamageAttributeProperty", menuName = "Attributes/DamageAttributeProperty")]
public class DamageAttributeProperty : ScriptableObject {
    [field: SerializeField]
    public AttributeRange DamageAttribute { get; private set; }
    [field: SerializeField]
    public AttributeInteger DamageAttributeResistance { get; private set; }
    [field: SerializeField]
    public float BlockedDamagePerOneResistance { get; private set; }
}
