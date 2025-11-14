using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemsData {
    public List<ItemData> itemsData = new List<ItemData>();
}

[Serializable]
public class ItemData {
    public ItemCategory itemCategory;
    public string name;
    public string description;
    public int price;
    public List<ItemAttributeData> attributesData = new List<ItemAttributeData>();
    public ItemType itemType;
    public BodyType bodyType;
    public float duration;

    public ItemData(Item item) {
        itemCategory = item.ItemCategory;
        name = item.Name;
        description = item.Description;
        price = item.Price;

        if (itemCategory.Equals(ItemCategory.Wearable)) {
            itemType = item.ItemType;
            bodyType = item.BodyType;
        }
        else {
            duration = item.Duration;
        }
    }
}

[Serializable]
public class ItemAttributeData {
    public ValueForm valueForm;
    public ModifierType modifierType;
    public AttributeType attributeType;
    public float fixedValue;
    public float rangeMinValue;
    public float rangeMaxValue;

    public ItemAttributeData(ItemAttribute itemAttribute) {
        valueForm = itemAttribute.ValueForm;
        modifierType = itemAttribute.ModifierType;
        attributeType = itemAttribute.AttributeType;

        if (valueForm.Equals(ValueForm.Fixed)) {
            fixedValue = itemAttribute.FixedValue;
        }
        else {
            rangeMinValue = itemAttribute.RangeMinValue;
            rangeMaxValue = itemAttribute.RangeMaxValue;
        }
    }
}
