using UnityEngine;

[CreateAssetMenu(fileName = "PotionAction", menuName = "Item/Actions/PotionAction")]
public class PotionAction : ItemAction {
    [SerializeField]
    private Item _item;

    public override void Execute() {
        EventManager.OnItemDressedHandler(_item);
        EventManager.UseItemEventHandler(_item);
    }
}
