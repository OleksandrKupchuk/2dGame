using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject {
    [field: SerializeField]
    public float Health { get; private set; }
    [field: SerializeField]
    public float Speed { get; private set; }
    [field: SerializeField]
    public float JumpVelocity { get; private set; }
    [field: SerializeField]
    public float Armor { get; private set; }
    [field: SerializeField]
    public float PhysicalDamageMin { get; private set; }
    [field: SerializeField]
    public float PhysicalDamageMax { get; private set; }
    [field: SerializeField]
    public float HealthRegeneration { get; private set; }
    [field: SerializeField]
    public float DelayHealthRegeneration { get; private set; }
    [field: SerializeField]
    public int Coins { get; set; }
}
