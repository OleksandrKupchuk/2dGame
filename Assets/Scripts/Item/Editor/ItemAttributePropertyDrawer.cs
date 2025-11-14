using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemAttribute))]
public class ItemAttributePropertyDrawer : PropertyDrawer {
    private float _positionY;
    private float _fieldHeight = EditorGUIUtility.singleLineHeight;
    private AttributeType[] _allowedAttributeTypes;

    private SerializedProperty _iconProperty;
    private SerializedProperty _attributeTypeProperty;
    private SerializedProperty _valueFormProperty;
    private SerializedProperty _modifierTypeProperty;
    private SerializedProperty _fixedMinProperty;
    private SerializedProperty _fixedMaxProperty;
    private SerializedProperty _rangeMinLowerProperty;
    private SerializedProperty _rangeMaxLowerProperty;
    private SerializedProperty _rangeMinUpperProperty;
    private SerializedProperty _rangeMaxUpperProperty;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        Rect _foldoutPosition = new Rect(position.x, position.y, position.size.x, _fieldHeight);
        property.isExpanded = EditorGUI.Foldout(_foldoutPosition, property.isExpanded, label);

        if (!property.isExpanded) {
            return;
        }

        _attributeTypeProperty = property.FindPropertyRelative("_attributeType");
        _fixedMinProperty = property.FindPropertyRelative("_fixedMin");
        _fixedMaxProperty = property.FindPropertyRelative("_fixedMax");
        _rangeMinLowerProperty = property.FindPropertyRelative("_rangeMinLower");
        _rangeMaxLowerProperty = property.FindPropertyRelative("_rangeMaxLower");
        _rangeMinUpperProperty = property.FindPropertyRelative("_rangeMinUpper");
        _rangeMaxUpperProperty = property.FindPropertyRelative("_rangeMaxUpper");

        _positionY = 0f;

        DrawIconFieldAndTryLoadSprite(position, property);
        DrawValueFormFieldAndCreateFilteredAttributeTypes(position, property);
        DrawModifierTypeField(position, property);
        DrawAttributeTypeField(position);

        if (_valueFormProperty.enumValueIndex == (int)ValueForm.Fixed) {
            _rangeMinLowerProperty.floatValue = 0f;
            _rangeMaxLowerProperty.floatValue = 0f;
            _rangeMinUpperProperty.floatValue = 0f;
            _rangeMaxUpperProperty.floatValue = 0f;

            DrawFloatField(position, _fixedMinProperty, label);
            DrawFloatField(position, _fixedMaxProperty, label);

            if (_fixedMinProperty.floatValue > _fixedMaxProperty.floatValue) {
                _fixedMinProperty.floatValue = _fixedMaxProperty.floatValue;
            }
        }
        else {
            _fixedMinProperty.floatValue = 0f;
            _fixedMaxProperty.floatValue = 0f;

            DrawFloatField(position, _rangeMinLowerProperty, label);
            DrawFloatField(position, _rangeMaxLowerProperty, label);
            DrawFloatField(position, _rangeMinUpperProperty, label);
            DrawFloatField(position, _rangeMaxUpperProperty, label);

            if (_rangeMinLowerProperty.floatValue > _rangeMaxLowerProperty.floatValue) {
                _rangeMinLowerProperty.floatValue = _rangeMaxLowerProperty.floatValue;
            }

            if (_rangeMaxLowerProperty.floatValue > _rangeMinUpperProperty.floatValue) {
                _rangeMaxLowerProperty.floatValue = _rangeMinUpperProperty.floatValue;
            }

            if (_rangeMinUpperProperty.floatValue > _rangeMaxUpperProperty.floatValue) {
                _rangeMinUpperProperty.floatValue = _rangeMaxUpperProperty.floatValue;
            }
        }

        EditorGUI.EndProperty();
    }

    private void DrawIconFieldAndTryLoadSprite(Rect position, SerializedProperty property) {
        _iconProperty = property.FindPropertyRelative("_icon");
        _positionY += position.y + _fieldHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _fieldHeight);

        string _spritePath = "Sprites/Attributes/" + _attributeTypeProperty.enumNames[_attributeTypeProperty.intValue];
        Sprite _icon = Resources.Load<Sprite>(_spritePath);

        if (_icon != null) {
            _iconProperty.objectReferenceValue = _icon;
            GUI.enabled = false;
            EditorGUI.PropertyField(_position, _iconProperty);
            GUI.enabled = true;
        }
        else {
            Debug.LogWarning($"Can't load sprite with name {_attributeTypeProperty.enumNames[_attributeTypeProperty.intValue]}, please check the path '{ResourcesPath.FolderTooltip + _attributeTypeProperty.enumNames[_attributeTypeProperty.intValue]}'");
            EditorGUI.PropertyField(_position, _iconProperty);
        }
    }

    private void DrawValueFormFieldAndCreateFilteredAttributeTypes(Rect position, SerializedProperty property) {
        _valueFormProperty = property.FindPropertyRelative("_valueForm");
        _positionY += _fieldHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _fieldHeight);
        EditorGUI.PropertyField(_position, _valueFormProperty);

        if(_valueFormProperty.enumValueIndex == (int)ValueForm.Fixed) {
            _allowedAttributeTypes = new[] { AttributeType.Armor, AttributeType.Health, AttributeType.Speed, AttributeType.HealthRegeneration,
                AttributeType.FireResistance};
        }
        else {
            _allowedAttributeTypes = new[] { AttributeType.PhysicalDamage, AttributeType.FireDamage, AttributeType.FrostDamage,
                AttributeType.LightingDamage, AttributeType.PoisonDamage, AttributeType.MagicDamage};
        }
    }

    private void DrawModifierTypeField(Rect position, SerializedProperty property) {
        _modifierTypeProperty = property.FindPropertyRelative("_modifierType");
        _positionY += _fieldHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, _fieldHeight);
        EditorGUI.PropertyField(_position, _modifierTypeProperty);
    }

    private void DrawAttributeTypeField(Rect position) {
        string[] _attributeTypes = _allowedAttributeTypes.Select(x => x.ToString()).ToArray();
        int _currentIndex = System.Array.IndexOf(_allowedAttributeTypes, (AttributeType)_attributeTypeProperty.enumValueIndex);
        if (_currentIndex < 0) _currentIndex = 0;

        _positionY += _fieldHeight + EditorGUIUtility.standardVerticalSpacing;

        Rect _position = new Rect(position.x, _positionY, position.width, _fieldHeight);
        int _attributeTypeIndex = EditorGUI.Popup(_position, "Attribute Type", _currentIndex, _attributeTypes);

        _attributeTypeProperty.enumValueIndex = (int)_allowedAttributeTypes[_attributeTypeIndex];
    }

    private void DrawFloatField(Rect position, SerializedProperty property, GUIContent label) {
        _positionY += _fieldHeight + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width - 20, _fieldHeight);
        EditorGUI.PropertyField(_position, property);

        Rect _helpButtonRect = new Rect(position.x + position.width - _fieldHeight, _positionY, _fieldHeight, _fieldHeight);
        GUIContent _helpIcon = EditorGUIUtility.IconContent("_Help");
        _helpIcon.tooltip = "Min fields never be more than Max fields";

        GUI.Button(_helpButtonRect, _helpIcon, GUIStyle.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        _iconProperty = property.FindPropertyRelative("_icon");
        _attributeTypeProperty = property.FindPropertyRelative("_attributeType");
        _valueFormProperty = property.FindPropertyRelative("_valueForm");
        _modifierTypeProperty = property.FindPropertyRelative("_modifierType");

        if (!property.isExpanded) {
            return _fieldHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        float _height = _fieldHeight + EditorGUIUtility.standardVerticalSpacing;

        _height += EditorGUI.GetPropertyHeight(_iconProperty) + EditorGUIUtility.standardVerticalSpacing;
        _height += EditorGUI.GetPropertyHeight(_valueFormProperty) + EditorGUIUtility.standardVerticalSpacing;
        _height += EditorGUI.GetPropertyHeight(_modifierTypeProperty) + EditorGUIUtility.standardVerticalSpacing;
        _height += EditorGUI.GetPropertyHeight(_attributeTypeProperty) + EditorGUIUtility.standardVerticalSpacing;

        if (_valueFormProperty.enumValueIndex == (int)ValueForm.Fixed) {
            int _amountOfFixedFields = 2;
            _height += (EditorGUIUtility.singleLineHeight * _amountOfFixedFields) + (EditorGUIUtility.standardVerticalSpacing * _amountOfFixedFields);
        }
        else {
            int _amountOfRangeFields = 4;
            _height += (EditorGUIUtility.singleLineHeight * _amountOfRangeFields) + (EditorGUIUtility.standardVerticalSpacing * _amountOfRangeFields);
        }

        return _height;
    }
}
