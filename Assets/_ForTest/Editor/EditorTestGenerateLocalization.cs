using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TestGenerateLocalization))]
public class EditorTestGenerateLocalization : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty _titleProperty = property.FindPropertyRelative("_title");
        SerializedProperty _descriptionProperty = property.FindPropertyRelative("_description");

        EditorGUILayout.PropertyField(_titleProperty, new GUIContent("Title"));
        EditorGUILayout.PropertyField(_descriptionProperty, new GUIContent("Description"));

        if(GUILayout.Button("Create")) {
            string tableName = _titleProperty.stringValue;
            LocalizationTable localizationTable = new LocalizationTable("Test", tableName);
            localizationTable.AddEntry("testTitle", tableName);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return base.GetPropertyHeight(property, label);
    }
}
