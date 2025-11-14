using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterAttributes", menuName = "Character/Attributes")]
public class CharacterAttributes : ScriptableObject {
    [field: SerializeField]
    public CharacterFixedAttribute Health { get; private set; }
    [field: SerializeField]
    public CharacterFixedAttribute HealthRegeneration { get; private set; }
    [field: SerializeField]
    public CharacterFixedAttribute Speed { get; private set; }
    [field: SerializeField]
    public List<CharacterDamageAttributeProperty> DamageAttributeProperties { get; private set; }
}
