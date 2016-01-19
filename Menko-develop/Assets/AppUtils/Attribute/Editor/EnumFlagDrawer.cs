using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(EnumFlag))]
public class EnumFlagDrawer : PropertyDrawer
{
	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		int nowValue = prop.intValue;

		var content = EditorGUI.BeginProperty(pos, label, prop);
		prop.intValue = EditorGUI.MaskField(pos, content, nowValue, prop.enumNames);
		EditorGUI.EndProperty();
	}
}