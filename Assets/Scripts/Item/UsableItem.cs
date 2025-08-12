using UnityEngine;

[CreateAssetMenu(fileName = "UsableItem", menuName = "Item/UsableItem")]
public class UsableItem : Item {
    [SerializeField]
    private float _duration;

    public float Duration => _duration;

    public void Use() {
        EventManager.OnItemDressedHandler(this);
    }
}
