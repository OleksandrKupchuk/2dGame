using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject {
    [SerializeField]
    protected int _price;


    private void OnEnable() {
        _price = Random.Range(MinPrice, MaxPrice);
    }

    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    public int Price => _price;
    [field: SerializeField]
    public int MinPrice { get; private set; }
    [field: SerializeField]
    public int MaxPrice { get; private set; }
    [field: SerializeField]
    public Sprite Icon { get; set; }
    [field: SerializeField]
    public List<AttributeData> Attributes { get; protected set; } = new List<AttributeData>();
    [field: SerializeField]
    public List<AttributeDataBase> AttributeDataBase { get; set; } = new List<AttributeDataBase>();
}
