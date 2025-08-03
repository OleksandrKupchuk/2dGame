using System.Collections.Generic;
using UnityEngine;

public class CreationItem {
    public WearableItemData CreateWearableItemData(WearableItemData instance) {
        WearableItemData _itemData = ScriptableObject.Instantiate(instance);

        List<AttributeDataBase> _attributes = new List<AttributeDataBase>();

        foreach(AttributeDataBase attribute in _itemData.AttributeDataBase) {
            AttributeDataBase _attribute = ScriptableObject.Instantiate(attribute);
            _attributes.Add(_attribute);
        }

        _itemData.AttributeDataBase = _attributes;

        return _itemData;
    }

    public void CreateUsableItemData() {

    }

    public void CreateItemData() {

    }
}
