using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Npc : MonoBehaviour, IInteracvite {
    [SerializeField]
    protected GameObject _interactionIcon;
    [SerializeField]
    protected NpcDialogues _dialogues;
    [SerializeField]
    protected DialogController _dialogController;

    public abstract void Interact();
}
