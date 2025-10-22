using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(ItemAttribute))]
public class ItemAttributeEditor : Editor {
    private const string _spritesPath = "Sprites/CharacterAttributes/";
    private SerializedProperty _isRangeAttributeProperty;
    private SerializedProperty _valueMinProperty;
    private SerializedProperty _valueMaxProperty;
    private SerializedProperty _iconProperty;
    private SerializedProperty _attributeTypeProperty;
    private SerializedProperty _valueTypeProperty;
    private SerializedProperty _valueMinRangeMinProperty;
    private SerializedProperty _valueMinRangeMaxProperty;
    private SerializedProperty _valueMaxRangeMinProperty;
    private SerializedProperty _valueMaxRangeMaxProperty;

    public override void OnInspectorGUI() {
        _isRangeAttributeProperty = serializedObject.FindProperty("_isRangeAttribute");
        _iconProperty = serializedObject.FindProperty("_icon");
        _attributeTypeProperty = serializedObject.FindProperty("_attributeType");
        _valueTypeProperty = serializedObject.FindProperty("_valueType");
        _valueMinProperty = serializedObject.FindProperty("_valueMin");
        _valueMaxProperty = serializedObject.FindProperty("_valueMax");
        _valueMinRangeMinProperty = serializedObject.FindProperty("_valueMinRangeMin");
        _valueMinRangeMaxProperty = serializedObject.FindProperty("_valueMinRangeMax");
        _valueMaxRangeMinProperty = serializedObject.FindProperty("_valueMaxRangeMin");
        _valueMaxRangeMaxProperty = serializedObject.FindProperty("_valueMaxRangeMax");

        serializedObject.Update();

        EditorGUILayout.PropertyField(_isRangeAttributeProperty);
        EditorGUILayout.PropertyField(_attributeTypeProperty);
        EditorGUILayout.PropertyField(_valueTypeProperty);
        EditorGUILayout.PropertyField(_iconProperty);

        if (!_isRangeAttributeProperty.boolValue) {
            EditorGUILayout.PropertyField(_valueMinProperty);
            EditorGUILayout.PropertyField(_valueMaxProperty);

            if (_valueMinProperty.floatValue > _valueMaxProperty.floatValue) {
                _valueMinProperty.floatValue = _valueMaxProperty.floatValue;
            }
        }
        else {
            EditorGUILayout.PropertyField(_valueMinRangeMinProperty);
            EditorGUILayout.PropertyField(_valueMinRangeMaxProperty);
            EditorGUILayout.PropertyField(_valueMaxRangeMinProperty);
            EditorGUILayout.PropertyField(_valueMaxRangeMaxProperty);

            if (_valueMinRangeMinProperty.floatValue > _valueMinRangeMaxProperty.floatValue) {
                _valueMinRangeMinProperty.floatValue = _valueMinRangeMaxProperty.floatValue;
            }

            if(_valueMinRangeMaxProperty.floatValue > _valueMaxRangeMinProperty.floatValue) {
                _valueMinRangeMaxProperty.floatValue = _valueMaxRangeMinProperty.floatValue;
            }

            if (_valueMaxRangeMinProperty.floatValue > _valueMaxRangeMaxProperty.floatValue) {
                _valueMaxRangeMinProperty.floatValue = _valueMaxRangeMaxProperty.floatValue;
            }
        }

        string _path = _spritesPath + _attributeTypeProperty.enumNames[_attributeTypeProperty.intValue];
        Sprite _icon = Resources.Load<Sprite>(_path);

        if (_icon != null) {
            _iconProperty.objectReferenceValue = _icon;
        }
        else {
            Debug.Log($"can't load sprite? please check the path '{ResourcesPath.FolderTooltip + _attributeTypeProperty.enumNames[_attributeTypeProperty.intValue]}'");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
