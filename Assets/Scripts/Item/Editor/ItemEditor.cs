using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {
    private SerializedProperty _itemTypeProperty;
    private SerializedProperty _nameProperty;
    private SerializedProperty _descriptionProperty;
    private SerializedProperty _minPriceProperty;
    private SerializedProperty _maxPriceProperty;
    private SerializedProperty _iconProperty;
    private SerializedProperty _attributesProperty;
    private SerializedProperty _isNeedDurationProperty;
    private SerializedProperty _durationProperty;
    private SerializedProperty _itemTypeAttributeProperty;
    private SerializedProperty _bodyTypeProperty;
    private SerializedProperty _itemActionsProperty;
    private SerializedProperty _spawnChanceProperty;

    public override void OnInspectorGUI() {
        Item _item = (Item)target;

        _itemTypeProperty = serializedObject.FindProperty("_itemCategory");
        _nameProperty = serializedObject.FindProperty("_name");
        _descriptionProperty = serializedObject.FindProperty("_description");
        _minPriceProperty = serializedObject.FindProperty("_minPrice");
        _maxPriceProperty = serializedObject.FindProperty("_maxPrice");
        _iconProperty = serializedObject.FindProperty("_icon");
        _attributesProperty = serializedObject.FindProperty("_attributes");
        _isNeedDurationProperty = serializedObject.FindProperty("_isNeedDuration");
        _durationProperty = serializedObject.FindProperty("_duration");
        _itemTypeAttributeProperty = serializedObject.FindProperty("_itemType");
        _bodyTypeProperty = serializedObject.FindProperty("_bodyType");
        _itemActionsProperty = serializedObject.FindProperty("_itemActions");
        _spawnChanceProperty = serializedObject.FindProperty("_spawnChance");

        serializedObject.Update();

        EditorGUILayout.PropertyField(_itemTypeProperty);
        EditorGUILayout.PropertyField(_nameProperty);
        EditorGUILayout.PropertyField(_descriptionProperty);
        EditorGUILayout.PropertyField(_minPriceProperty);
        EditorGUILayout.PropertyField(_maxPriceProperty);
        EditorGUILayout.PropertyField(_iconProperty);
        EditorGUILayout.PropertyField(_attributesProperty);
        EditorGUILayout.PropertyField(_bodyTypeProperty);

        if (_itemTypeProperty.intValue == (int)ItemCategory.Usable) {
            EditorGUILayout.PropertyField(_itemActionsProperty);
            EditorGUILayout.PropertyField(_isNeedDurationProperty);

            if (_isNeedDurationProperty.boolValue) {
                EditorGUILayout.PropertyField(_durationProperty);
            }
            else {
                _durationProperty.floatValue = 0f;
            }
        }
        else {
            _itemActionsProperty.ClearArray();

            EditorGUILayout.PropertyField(_itemTypeAttributeProperty);
        }

        EditorGUILayout.PropertyField(_spawnChanceProperty);
        serializedObject.ApplyModifiedProperties();
    }
}
