using System.Collections.Generic;
using UnityEngine;

public class CreationItem {
    public void CreateItemData(Item instance) {
        if (instance.ItemType.Equals(ItemType.Wearable)) {
            Item _item = ScriptableObject.Instantiate(instance);

            List<ItemAttribute> _attributes = new List<ItemAttribute>();

            foreach (ItemAttribute attribute in _item.Attributes) {
                ItemAttribute _attribute = ScriptableObject.Instantiate(attribute);
                _attributes.Add(_attribute);
            }

            _item.SetAttributes(_attributes);
        }
    }

    public Item CreateWearableItemData(Item instance) {
        Item _itemData = ScriptableObject.Instantiate(instance);

        List<ItemAttribute> _attributes = new List<ItemAttribute>();

        foreach(ItemAttribute attribute in _itemData.Attributes) {
            ItemAttribute _attribute = ScriptableObject.Instantiate(attribute);
            _attributes.Add(_attribute);
        }

        _itemData.SetAttributes(_attributes);

        return _itemData;
    }
}
