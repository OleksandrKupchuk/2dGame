using UnityEditor;

//[CustomEditor(typeof(Item))]
public class ItemEditor : Editor {
    private SerializedProperty _itemTypeProperty;
    private SerializedProperty _nameProperty;
    private SerializedProperty _descriptionProperty;
    private SerializedProperty _minPriceProperty;
    private SerializedProperty _maxPriceProperty;
    private SerializedProperty _iconProperty;
    private SerializedProperty _attributesProperty;
    private SerializedProperty _durationProperty;
    private SerializedProperty _itemTypeAttributeProperty;
    private SerializedProperty _bodyTypeProperty;
    private SerializedProperty _itemActionsProperty;

    public override void OnInspectorGUI() {
        _itemTypeProperty = serializedObject.FindProperty("_itemType");
        _nameProperty = serializedObject.FindProperty("_name");
        _descriptionProperty = serializedObject.FindProperty("_description");
        _minPriceProperty = serializedObject.FindProperty("_minPrice");
        _maxPriceProperty = serializedObject.FindProperty("_maxPrice");
        _iconProperty = serializedObject.FindProperty("_icon");
        _attributesProperty = serializedObject.FindProperty("_attributes");
        _durationProperty = serializedObject.FindProperty("_duration");
        _itemTypeAttributeProperty = serializedObject.FindProperty("_itemTypeAttribute");
        _bodyTypeProperty = serializedObject.FindProperty("_bodyType");
        _itemActionsProperty = serializedObject.FindProperty("_itemActions");

        serializedObject.Update();

        EditorGUILayout.PropertyField(_itemTypeProperty);
        EditorGUILayout.PropertyField(_nameProperty);
        EditorGUILayout.PropertyField(_descriptionProperty);
        EditorGUILayout.PropertyField(_minPriceProperty);
        EditorGUILayout.PropertyField(_maxPriceProperty);
        EditorGUILayout.PropertyField(_iconProperty);
        EditorGUILayout.PropertyField(_attributesProperty);
        EditorGUILayout.PropertyField(_itemTypeAttributeProperty);
        EditorGUILayout.PropertyField(_bodyTypeProperty);

        if (_itemTypeProperty.intValue == (int)ItemType.Usable) {
            EditorGUILayout.PropertyField(_durationProperty);
            EditorGUILayout.PropertyField(_itemActionsProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
