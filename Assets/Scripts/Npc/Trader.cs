using System.Collections.Generic;
using UnityEngine;

public class Trader : Npc {
    private ItemGeneration _itemGeneration = new ItemGeneration();

    [SerializeField]
    private int _traderCommissionInPercent;
    [SerializeField]
    private Market _market;
    [SerializeField]
    private List<Item> _traderItems;

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
        if (!_dialogController.IsDialoguesOpen) {
            if (!_market.IsMarketChecked) {
                _market.SetItems(_itemGeneration.GenerateDropItems(_traderItems));
                _market.SetCommission(_traderCommissionInPercent);
            }

            _dialogController.OpenDialogues(gameObject.name, _dialogues);
        }
    }
}
