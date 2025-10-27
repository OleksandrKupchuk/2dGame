using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemsData {
    public List<ItemData> itemsData = new List<ItemData>();
}

[System.Serializable]
public class ItemData {
    public string itemType;
    public string name;
    public string description;
    public Sprite icon;
    public int price;
    public List<ItemAttributeData> _attributesData = new List<ItemAttributeData>();
    public string itemTypeAttribute;
    public string bodyType;
    public float duration;
}

[System.Serializable]
public class ItemAttributeData {
    public ValueForm valueForm;
    public ModifierType modifierType;
    public AttributeType attributeType;
    public Sprite icon;
    public float fixedValue;
    public float rangeMinValue;
    public float rangeMaxValue;
}
