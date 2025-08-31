using TMPro;
using UnityEngine;

public class DamageView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _damage;

    public void ShowDamage(float damage, Color color) {
        _damage.color = color;
        _damage.text = $"{string.Format("{0:0.0}", damage)}";
    }
}
