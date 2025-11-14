using UnityEngine;
using UnityEngine.UI;

public class TakeAwayPlayerHealth : MonoBehaviour {
    [SerializeField]
    private Button _addHealthButton;
    [SerializeField]
    private CharacterAttributes _attributes;
    [SerializeField]
    private Damage _damage;

    private void Start() {
        DamageController _damageController = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageController>();

        _addHealthButton.onClick.AddListener(() => {
            _damageController.TryTakeDamage(_attributes.DamageAttributeProperties, _damage);
        });
    }
}
