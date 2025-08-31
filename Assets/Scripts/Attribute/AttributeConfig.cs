using UnityEngine;

[CreateAssetMenu(fileName = "AttributeConfig", menuName = "Config/AttributeConfig")]
public class AttributeConfig : ScriptableObject {
    [field: SerializeField]
    public float Health { get; set; }
    [field: SerializeField]
    public float HealthRegeneration { get; set; }
    [field: SerializeField]
    public float Armor { get; set; }
    [field: SerializeField]
    public float Speed { get; set; }
    [field: SerializeField]
    public float PhysicalDamageMin { get; set; }
    [field: SerializeField]
    public float PhysicalDamageMax { get; set; }
    [field: SerializeField]
    public float FireDamageMin { get; set; }
    [field: SerializeField]
    public float FireDamageMax { get; set; }
    [field: SerializeField]
    public float FireResistance { get; set; }
}
