using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventorySaveLoadSystem : MonoBehaviour {
    private SaveLoadJsonSystem _saveLoadJsonSystem = new SaveLoadJsonSystem();

    [SerializeField]
    private Inventory _inventory;

    public void SaveInventory() {
        string _filePath = Application.persistentDataPath + $"/Inventory.json";
        File.Delete(_filePath);

        ItemsData _wraperItemsData = new ItemsData();

        foreach (Item item in _inventory.Items) {
            _wraperItemsData.itemsData.Add(GetItemData(item));
        }

        _saveLoadJsonSystem.Save("Inventory", _wraperItemsData);
        print("Inventory saved successfully.");
    }

    private ItemData GetItemData(Item item) {
        ItemData _itemData = new ItemData();

        _itemData.itemType = item.ItemCategory.ToString();
        _itemData.name = item.Name;
        _itemData.description = item.Description;
        _itemData.price = item.Price;
        _itemData._attributesData = GetItemsAttributesData(item.Attributes);

        if (item.ItemCategory.Equals(ItemCategory.Wearable)) {
            _itemData.itemTypeAttribute = item.ItemTypeAttribute.ToString();
            _itemData.bodyType = item.BodyType.ToString();
        }
        else {
            _itemData.duration = item.Duration;
        }

        return _itemData;
    }

    private List<ItemAttributeData> GetItemsAttributesData(List<ItemAttribute> itemsAttributes) {
        List<ItemAttributeData> _itemsAttributes = new List<ItemAttributeData>();

        foreach (ItemAttribute itemAttribute in itemsAttributes) {
            ItemAttributeData _itemAttributes = new ItemAttributeData();

            _itemAttributes.valueForm = itemAttribute.ValueForm;
            _itemAttributes.attributeType = itemAttribute.AttributeType;
            _itemAttributes.modifierType = itemAttribute.ModifierType;

            if (itemAttribute.ValueForm.Equals(ValueForm.Range)) {
                _itemAttributes.rangeMinValue = itemAttribute.RangeMinValue;
                _itemAttributes.rangeMaxValue = itemAttribute.RangeMaxValue;
            }
            else {
                _itemAttributes.fixedValue = itemAttribute.FixedValue;
            }

            _itemsAttributes.Add(_itemAttributes);
        }

        return _itemsAttributes;
    }

    public void LoadInventory() {
        try {

            ItemsData _itemsData = _saveLoadJsonSystem.Load<ItemsData>("Inventory");

            foreach (var itemData in _itemsData.itemsData) {
                Item _item = LoadItem(itemData);
                _inventory.TryAddItem(_item);
            }

            print("Inventory loaded successfully.");
        }
        catch (System.Exception e) {
            Debug.LogError(e.Message);
        }
    }

    private Item LoadItem(ItemData itemData) {
        Item _item = ScriptableObject.CreateInstance<Item>();

        _item.ItemCategory = (ItemCategory)Enum.Parse(typeof(ItemCategory), itemData.itemType);
        _item.Name = itemData.name;
        _item.Description = itemData.description;
        _item.Price = itemData.price;
        string _iconPath = $"Sprites/Items/{itemData.name}";
        _item.Icon = Resources.Load<Sprite>(_iconPath);
        _item.Attributes = LoadItemAttributes(itemData._attributesData);

        if (_item.ItemCategory.Equals(ItemCategory.Wearable)) {
            _item.ItemTypeAttribute = (ItemType)Enum.Parse(typeof(ItemType), itemData.itemTypeAttribute);
            _item.BodyType = (BodyType)Enum.Parse(typeof(BodyType), itemData.bodyType);
        }
        else {
            _item.Duration = itemData.duration;
        }

        return _item;
    }

    private List<ItemAttribute> LoadItemAttributes(List<ItemAttributeData> itemsAttributesData) {
        List<ItemAttribute> _itemAttributes = new List<ItemAttribute>();

        foreach (var itemAttributesData in itemsAttributesData) {
            ItemAttribute _itemAttribute = new ItemAttribute();
            string _iconPath = $"Sprites/Attributes/{itemAttributesData.attributeType}";
            _itemAttribute.Icon = Resources.Load<Sprite>(_iconPath);
            _itemAttribute.ValueForm = itemAttributesData.valueForm;
            _itemAttribute.AttributeType = itemAttributesData.attributeType;
            _itemAttribute.ModifierType = itemAttributesData.modifierType;

            if (itemAttributesData.valueForm.Equals(ValueForm.Range)) {
                _itemAttribute.RangeMinValue = itemAttributesData.rangeMinValue;
                _itemAttribute.RangeMaxValue = itemAttributesData.rangeMaxValue;
                _itemAttributes.Add(_itemAttribute);
            }
            else {
                _itemAttribute.FixedValue = itemAttributesData.fixedValue;
                _itemAttributes.Add(_itemAttribute);
            }
        }

        return _itemAttributes;
    }
}
