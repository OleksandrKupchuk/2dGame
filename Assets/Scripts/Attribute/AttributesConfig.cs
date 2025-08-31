using UnityEngine;

[CreateAssetMenu(fileName = "AttributesConfig", menuName = "Attributes/AttributesConfig")]
public class AttributesConfig : ScriptableObject {
    [field: SerializeField]
    public float Health { get; private set; }
    [field: SerializeField]
    public float HealthRegeneration { get; private set; }
    [field: SerializeField]
    public float Speed { get; private set; }
    [field: SerializeField]
    public float Armor { get; private set; }
    [field: SerializeField]
    public float PhysicalDamageMin { get; private set; }
    [field: SerializeField]
    public float PhysicalDamageMax { get; private set; }
    [field: SerializeField]
    public float FireDamageMin { get; private set; }
    [field: SerializeField]
    public float FireDamageMax { get; private set; }
}
