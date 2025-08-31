using UnityEngine;

[CreateAssetMenu(fileName = "PotionAction", menuName = "ItemAction/PotionAction")]
public class PotionAction : ItemAction {
    [SerializeField]
    private Item _item;

    public override void Execute() {
        EventManager.OnItemDressedHandler(_item);
        EventManager.UseItemEventHandler(_item);
    }
}
