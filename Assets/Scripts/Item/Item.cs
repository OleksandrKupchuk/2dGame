using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item", order = 0)]
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
    private float _duration;
    [SerializeField]
    private ItemTypeAttribute _itemTypeAttribute;
    [SerializeField]
    private BodyType _bodyType;

    public ItemType ItemType => _itemType;
    public string Name => _name;
    public string Description => _description;
    public int Price => _price;
    public Sprite Icon => _icon;
    public List<ItemAttribute> Attributes => _attributes;
    public float Duration => _duration;
    public ItemTypeAttribute ItemTypeAttribute => _itemTypeAttribute;
    public BodyType BodyType => _bodyType;

    public void Use() {
        EventManager.OnItemDressedHandler(this);
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
