using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Room))]
public class RoomPropertyDrawer : PropertyDrawer {
    private SerializedProperty _squareProperty;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);
        _squareProperty = property.FindPropertyRelative("_square");
        float _positionY = position.y + EditorGUIUtility.standardVerticalSpacing;
        Rect _position = new Rect(position.x, _positionY, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(_position, _squareProperty);
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        _squareProperty = property.FindPropertyRelative("_square");
        return EditorGUI.GetPropertyHeight(_squareProperty) + (2 * EditorGUIUtility.standardVerticalSpacing);
    }
}
