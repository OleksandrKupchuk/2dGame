using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Npc : MonoBehaviour, IInteracvite {
    [SerializeField]
    protected GameObject _interactionIcon;
    public abstract void Interact();
}
