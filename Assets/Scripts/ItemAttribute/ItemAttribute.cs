using UnityEngine;

[System.Serializable]
public abstract class ItemAttribute : ScriptableObject {
    protected bool _isRangeAttribute;

    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected AttributeType _attributeType;
    [SerializeField]
    protected ValueType _valueType;

    public bool IsRangeAttribute { get => _isRangeAttribute; set => _isRangeAttribute = value; }
    public AttributeType AttributeType { get => _attributeType; set => _attributeType = value; }
    public ValueType ValueType { get => _valueType; set => _valueType = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }

    protected void OnEnable() {
        GenerateParameters();
        OnValidate();
    }

    public abstract void GenerateParameters();

    protected void OnValidate() {
        switch (_attributeType) {
            case AttributeType.Armor:
                LoadIcon();
                break;
            case AttributeType.Health:
                LoadIcon();
                break;
            case AttributeType.Speed:
                LoadIcon();
                break;
            case AttributeType.HealthRegeneration:
                LoadIcon();
                break;
            case AttributeType.PhysicalDamage:
                LoadIcon();
                break;
            case AttributeType.FireDamage:
                LoadIcon();
                break;
            case AttributeType.FrostDamage:
                LoadIcon();
                break;
            case AttributeType.LightingDamage:
                LoadIcon();
                break;
            case AttributeType.PoisonDamage:
                LoadIcon();
                break;
            case AttributeType.MagicDamage:
                LoadIcon();
                break;
            case AttributeType.FireResistance:
                LoadIcon();
                break;
            default:
                Debug.LogWarning("Can not load item attribute image");
                break;
        }
    }

    protected void LoadIcon() {
        string _iconPath = $"Sprites/Attributes/{_attributeType}";
        _icon = Resources.Load<Sprite>(_iconPath);
    }

    public abstract string GetValue();
}

public enum AttributeType {
    Armor,
    Health,
    Speed,
    HealthRegeneration,
    PhysicalDamage,
    FireDamage,
    FrostDamage,
    LightingDamage,
    PoisonDamage,
    MagicDamage,
    FireResistance,
}

public enum ValueType {
    Integer,
    Percent
}
