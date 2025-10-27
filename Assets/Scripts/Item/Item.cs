using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item", order = 0)]
public class Item : ScriptableObject {
    [SerializeField, HideInInspector]
    private int _price;

    private void OnEnable() {
        _price = Random.Range(_minPrice, _maxPrice);
        Debug.Log($"Item <color=green>{_name}</color> have the price <color=yellow>{_price}</color> coins");
    }

    [SerializeField]
    private ItemCategory _itemCategory;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private int _minPrice;
    [SerializeField]
    private int _maxPrice;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private List<ItemAttribute> _attributes;
    [SerializeField]
    private bool _isNeedDuration;
    [SerializeField]
    private float _duration;
    [SerializeField]
    private ItemType _itemType;
    [SerializeField]
    private BodyType _bodyType;
    [SerializeField]
    private List<ItemAction> _itemActions = new List<ItemAction>();
    [SerializeField, Range(0, 100)]
    private float _spawnChance;

    public ItemCategory ItemCategory { get => _itemCategory; set => _itemCategory = value; }
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int Price { get => _price; set => _price = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public List<ItemAttribute> Attributes { get => _attributes; set => _attributes = value; }
    public float Duration { get => _duration; set => _duration = value; }
    public ItemType ItemTypeAttribute { get => _itemType; set => _itemType = value; }
    public BodyType BodyType { get => _bodyType; set => _bodyType = value; }
    public List<ItemAction> ItemActions { get => _itemActions; }
    public float SpawnChance { get => _spawnChance; }

    public void Use() {
        _itemActions.ToList().ForEach(action => action.Execute());
    }
}

public enum ItemCategory {
    Wearable,
    Usable,
}

public enum ItemType {
    None,
    Helmet,
    Armor,
    Ring,
    Amulet,
    Weapon,
    Belt,
    Shield
}
