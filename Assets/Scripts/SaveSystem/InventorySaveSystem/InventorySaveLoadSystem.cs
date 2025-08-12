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

        foreach (Item item in _inventory.ItemsData) {
            _saveLoadJsonSystem.Save("Inventory", GetItemData(item));
        }

        print("Inventory saved successfully.");
    }

    private ItemData GetItemData(Item item) {
        ItemData _itemData = new ItemData();

        _itemData.itemType = item.ItemType.ToString();
        _itemData.name = item.name;
        _itemData.description = item.Description;
        _itemData.icon = item.Icon;
        _itemData.price = item.Price;
        _itemData.itemsAttributes = GetItemsAttributesData(item.Attributes);

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
            _itemAttributes.icon = itemAttribute.Icon;
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
            List<Item> _items = _saveLoadJsonSystem.Load<List<Item>>("Inventory");

            foreach (var item in _items) {
                _inventory.TryAddItem(item);
            }

            print("Inventory loaded successfully.");
        }
        catch (System.Exception e) {
            Debug.LogError(e.Message);
        }
    }
}
