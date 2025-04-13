using UnityEngine;

public class King : Npc, IInteracvite {
    [SerializeField]
    private NpcDialogues _dialogues;
    [SerializeField]
    private DialogController _dialogController;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            print("interactive SET = " + gameObject.name);
            _interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            print("interactive OUT = " + gameObject.name);
            _interactionIcon.SetActive(false);
        }
    }

    public override void Interact() {
        _dialogController.OpenDialogues(gameObject.name, _dialogues);
    }
}
