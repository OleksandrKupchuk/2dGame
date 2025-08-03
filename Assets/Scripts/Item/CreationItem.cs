using System.Collections.Generic;
using UnityEngine;

public class CreationItem {
    public WearableItemData CreateWearableItemData(WearableItemData instance) {
        WearableItemData _itemData = ScriptableObject.Instantiate(instance);

        List<AttributeData> _attributes = new List<AttributeData>();

        foreach(AttributeData attribute in _itemData.Attributes) {
            AttributeData _attribute = ScriptableObject.Instantiate(attribute);
            _attributes.Add(_attribute);
        }

        _itemData.Attributes = _attributes;

        return _itemData;
    }

    public void CreateUsableItemData() {

    }

    public void CreateItemData() {

    }
}
