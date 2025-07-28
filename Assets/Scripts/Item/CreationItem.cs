using UnityEngine;

public class CreationItem {
    public WearableItemData CreateWearableItemData(WearableItemData instance) {
        //WearableItemData _itemData = ScriptableObject.CreateInstance<WearableItemData>();
        WearableItemData _itemData = ScriptableObject.Instantiate(instance);

        return _itemData;
    }

    public void CreateUsableItemData() {

    }

    public void CreateItemData() {

    }
}
