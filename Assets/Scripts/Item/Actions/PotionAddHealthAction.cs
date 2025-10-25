using UnityEngine;

[CreateAssetMenu(fileName = "PotionAddHealthAction", menuName = "Item/Actions/PotionAddHealthAction")]
public class PotionAddHealthAction : ItemAction {
    [SerializeField]
    private HealthController _healthController;
    [SerializeField]
    private ItemAttribute _potionAttribute;

    public override void Execute() {
        _healthController.AddHealth(_potionAttribute.FixedValue);
    }
}
