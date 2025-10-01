using UnityEngine;

[CreateAssetMenu(fileName = "PotionAddHealthAction", menuName = "ItemAction/PotionAddHealthAction")]
public class PotionAddHealthAction : ItemAction {
    [SerializeField]
    private HealthController _healthController;
    [SerializeField]
    private ItemAttributeInteger _potionAttribute;

    public override void Execute() {
        _healthController.AddHealth(_potionAttribute.Value);
    }
}
