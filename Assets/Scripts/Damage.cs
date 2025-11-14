using UnityEngine;

public class Damage : MonoBehaviour {
    [field: SerializeField]
    public BoxCollider2D BoxCollider2D { get; private set; }
    [field: SerializeField]
    public CharacterAttributes Attributes { get; private set; }
}
