using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Floors))]
public class FloorCustomEditor : Editor {
    private SerializedProperty _roomsProperty;
    private Floors _floors;

    private void OnEnable() {
        _roomsProperty = serializedObject.FindProperty("_rooms");
        //_dialogues = target as NpcDialogues;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_roomsProperty);

        if (GUILayout.Button("Create")) {
            Debug.Log("Click");
        }

        serializedObject.ApplyModifiedProperties();
    }
}
