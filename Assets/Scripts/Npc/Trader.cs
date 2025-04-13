using UnityEngine;

public class Trader : Npc {
    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private NpcDialogues _dialogues;
    [SerializeField]
    private int _commissionPercent;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _interactionIcon.SetActive(false);
        }
    }

    public override void Interact() {
        _dialogController.OpenDialogues(gameObject.name, _dialogues);
    }
}
