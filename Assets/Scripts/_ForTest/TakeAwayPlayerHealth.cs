using UnityEngine;
using UnityEngine.UI;

public class TakeAwayPlayerHealth : MonoBehaviour {
    [SerializeField]
    private float _health;
    [SerializeField]
    private Button _addHealthButton;
    [SerializeField]
    private HealthController _healthController;
    [SerializeField]
    private Attributes _attributes;

    private void Start() {
        _addHealthButton.onClick.AddListener(() => {
            _healthController.TakeDamage(_attributes.DamageAttributeProperties);
        });
    }
}
