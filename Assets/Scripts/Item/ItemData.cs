using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData {
    public string itemType;
    public string name;
    public string description;
    public Sprite icon;
    public int price;
    public List<ItemAttributeData> itemsAttributes = new List<ItemAttributeData>();
    public string itemTypeAttribute;
    public string bodyType;
    public float duration;
}

[System.Serializable]
public class ItemAttributeData {
    public bool isRangeAttribute;
    public Sprite icon;
    public string attributeType;
    public string valueType;
    public float value;
    public float valueMinRange;
    public float valueMaxRange;
}
