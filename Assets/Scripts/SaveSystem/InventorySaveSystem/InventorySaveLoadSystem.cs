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

        foreach (Item item in _inventory.ItemsData) {
            _wraperItemsData.itemsData.Add(GetItemData(item));
        }

        _saveLoadJsonSystem.Save("Inventory", _wraperItemsData);
        print("Inventory saved successfully.");
    }

    private ItemData GetItemData(Item item) {
        ItemData _itemData = new ItemData();

        _itemData.itemType = item.ItemType.ToString();
        _itemData.name = item.Name;
        _itemData.description = item.Description;
        _itemData.price = item.Price;
        _itemData.itemsAttributesData = GetItemsAttributesData(item.Attributes);

        if (item.ItemType.Equals(ItemType.Wearable)) {
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

            _itemAttributes.isRangeAttribute = itemAttribute.IsRangeAttribute;
            _itemAttributes.attributeType = itemAttribute.AttributeType.ToString();
            _itemAttributes.valueType = itemAttribute.ValueType.ToString();

            if (itemAttribute.IsRangeAttribute) {
                ItemAttributeRange _itemAttributeRange = itemAttribute as ItemAttributeRange;

                if (_itemAttributeRange != null) {
                    _itemAttributes.valueMinRange = _itemAttributeRange.ValueMinRange;
                    _itemAttributes.valueMaxRange = _itemAttributeRange.ValueMaxRange;
                }
            }
            else {
                ItemAttributeInteger _itemAttributeInteger = itemAttribute as ItemAttributeInteger;

                if (_itemAttributeInteger != null) {
                    _itemAttributes.value = _itemAttributeInteger.Value;
                }
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

        _item.ItemType = (ItemType)Enum.Parse(typeof(ItemType), itemData.itemType);
        _item.Name = itemData.name;
        _item.Description = itemData.description;
        _item.Price = itemData.price;
        string _iconPath = $"Sprites/Items/{itemData.name}";
        _item.Icon = Resources.Load<Sprite>(_iconPath);
        _item.Attributes = LoadItemAttributes(itemData.itemsAttributesData);

        if (_item.ItemType.Equals(ItemType.Wearable)) {
            _item.ItemTypeAttribute = (ItemTypeAttribute)Enum.Parse(typeof(ItemTypeAttribute), itemData.itemTypeAttribute);
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
            if (itemAttributesData.isRangeAttribute) {
                ItemAttributeRange _itemAttributeRange = ScriptableObject.CreateInstance<ItemAttributeRange>();
                _itemAttributeRange.IsRangeAttribute = itemAttributesData.isRangeAttribute;
                string _iconPath = $"Sprites/Attributes/{itemAttributesData.attributeType}";
                _itemAttributeRange.Icon = Resources.Load<Sprite>(_iconPath);
                _itemAttributeRange.AttributeType = (AttributeType)Enum.Parse(typeof(AttributeType), itemAttributesData.attributeType);
                _itemAttributeRange.ValueType = (ValueType)Enum.Parse(typeof(ValueType), itemAttributesData.valueType);
                _itemAttributeRange.ValueMinRange = itemAttributesData.valueMinRange;
                _itemAttributeRange.ValueMaxRange = itemAttributesData.valueMaxRange;
                _itemAttributes.Add(_itemAttributeRange);
            }
            else {
                ItemAttributeInteger _itemAttributeInteger = ScriptableObject.CreateInstance<ItemAttributeInteger>();
                _itemAttributeInteger.IsRangeAttribute = itemAttributesData.isRangeAttribute;
                string _iconPath = $"Sprites/Attributes/{itemAttributesData.attributeType}";
                _itemAttributeInteger.Icon = Resources.Load<Sprite>(_iconPath);
                _itemAttributeInteger.AttributeType = (AttributeType)Enum.Parse(typeof(AttributeType), itemAttributesData.attributeType);
                _itemAttributeInteger.ValueType = (ValueType)Enum.Parse(typeof(ValueType), itemAttributesData.valueType);
                _itemAttributeInteger.Value = itemAttributesData.value;
                _itemAttributes.Add(_itemAttributeInteger);
            }
        }

        return _itemAttributes;
    }
}
