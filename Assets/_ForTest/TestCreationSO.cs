using UnityEngine;

public class TestCreationSO : MonoBehaviour {
    [SerializeField]
    private Item _item;
    [SerializeField]
    private Item _instance;

    private void Start() {
        CreationItem _creationItem = new CreationItem();
        _item = _creationItem.GetCreatedItem(_instance);
    }
}
