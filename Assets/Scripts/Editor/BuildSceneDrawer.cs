using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MainMenu.BuildScene))]
public class BuildSceneDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
		EditorGUI.BeginProperty(position, label, property);

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		var nameRect = new Rect(position.x, position.y, position.width * 0.5f, position.height);
		var buildIndexRect = new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.5f, position.height);

		EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"), GUIContent.none);
		EditorGUI.PropertyField(buildIndexRect, property.FindPropertyRelative("BuildIndex"), GUIContent.none);

		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty();
	}
}