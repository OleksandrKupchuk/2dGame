using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
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
    [SerializeField, SerializeReference]
    protected List<ItemAttribute> _attributes;

    public ItemType ItemType => _itemType;
    public string Name => _name;
    public string Description => _description;
    public int Price => _price;
    public Sprite Icon => _icon;
    public List<ItemAttribute> Attributes => _attributes;

    public void SetAttributes(List<ItemAttribute> attributes) {
        _attributes = attributes;
    }

    [ContextMenu("Save to JSON")]
    public void SaveToJson() {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.persistentDataPath + "/myData.json", json);
        Debug.Log("Saved to JSON");
    }
}

public enum ItemType {
    Wearable,
    Usable,
}
