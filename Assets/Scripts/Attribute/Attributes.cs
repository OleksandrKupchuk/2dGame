using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "Attributes/Attributes")]
public class Attributes : ScriptableObject {
    [field: SerializeField]
    public AttributeInteger Health { get; private set; }
    [field: SerializeField]
    public AttributeInteger HealthRegeneration { get; private set; }
    [field: SerializeField]
    public AttributeInteger Speed { get; private set; }
    [field: SerializeField]
    public List<DamageAttributeProperty> DamageAttributeProperties { get; private set; }
}
