using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item", order = 0)]
public class Item : ScriptableObject {
    [SerializeField, HideInInspector]
    protected int _price;

    protected void OnEnable() {
        _price = Random.Range(_minPrice, _maxPrice);
    }

    [SerializeField]
    protected ItemType _itemType;
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected int _minPrice;
    [SerializeField]
    protected int _maxPrice;
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected List<ItemAttribute> _attributes;
    [SerializeField]
    private bool _isNeedDuration;
    [SerializeField]
    protected float _duration;
    [SerializeField]
    protected ItemTypeAttribute _itemTypeAttribute;
    [SerializeField]
    protected BodyType _bodyType;
    [SerializeField]
    protected List<ItemAction> _itemActions = new List<ItemAction>();

    public ItemType ItemType { get => _itemType; set => _itemType = value; }
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int Price { get => _price; set => _price = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public List<ItemAttribute> Attributes { get => _attributes; set => _attributes = value; }
    public float Duration { get => _duration; set => _duration = value; }
    public ItemTypeAttribute ItemTypeAttribute { get => _itemTypeAttribute; set => _itemTypeAttribute = value; }
    public BodyType BodyType { get => _bodyType; set => _bodyType = value; }
    public List<ItemAction> ItemActions { get => _itemActions; }

    public void Use() {
        _itemActions.ToList().ForEach(action => action.Execute());
    }

    public void SetAttributes(List<ItemAttribute> attributes) {
        _attributes = attributes;
    }
}

public enum ItemType {
    Wearable,
    Usable,
}

public enum ItemTypeAttribute {
    None,
    Helmet,
    Armor,
    Ring,
    Amulet,
    Weapon,
    Belt,
    Shield
}
