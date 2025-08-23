using System.Collections.Generic;
using UnityEngine;

public class CreationItem {
    public Item GetCreatedItem(Item instance) {
        Item _item = ScriptableObject.Instantiate(instance);

        List<ItemAttribute> _attributes = new List<ItemAttribute>();

        foreach(ItemAttribute attribute in _item.Attributes) {
            ItemAttribute _attribute = ScriptableObject.Instantiate(attribute);
            _attributes.Add(_attribute);
        }

        _item.SetAttributes(_attributes);

        return _item;
    }
}
