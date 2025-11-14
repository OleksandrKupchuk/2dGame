using UnityEngine;

[CreateAssetMenu(fileName = "PotionAddHealthAction", menuName = "Item/Actions/PotionAddHealthAction")]
public class PotionAddHealthAction : ItemAction {
    [SerializeField]
    private HealthController _healthController;
    [SerializeField]
    private Item _healthPotion;

    public override void Execute() {
        _healthController.AddHealth(_healthPotion.Attributes[0].FixedValue);
    }
}
