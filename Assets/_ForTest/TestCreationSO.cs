using UnityEngine;

public class TestCreationSO : MonoBehaviour {
    [SerializeField]
    private WearableItemData _item;
    [SerializeField]
    private WearableItemData _instance;

    private void Start() {
        CreationItem _creationItem = new CreationItem();
        _item = _creationItem.CreateWearableItemData(_instance);
    }
}
