using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttributesProperty", menuName = "Attribute/AttributesProperty")]
public class AttributesProperty : ScriptableObject {
    [field: SerializeField]
    public AttributeInteger HealthAttribute { get; private set; }
    [field: SerializeField]
    public AttributeInteger SpeedAttribute { get; private set; }
    [field: SerializeField]
    public AttributeInteger HealthRegenerationAttribute { get; private set; }
    [field: SerializeField]
    public List<DamageProperty> DamageProperties { get; private set; } = new List<DamageProperty>();
}
