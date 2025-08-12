using UnityEngine;

public class TestScriptableObject : MonoBehaviour {
    [SerializeField]
    private ItemAttribute _attribute;

    void Start() {
        if (_attribute.Icon != null) {
            Debug.Log("name sprite = " + _attribute.Icon);
        }
        else {
            Debug.Log("icon is null");
        }
    }
}
