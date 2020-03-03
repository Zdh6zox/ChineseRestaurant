using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string valueStr;
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer: 
                valueStr = property.intValue.ToString();
                break;

            case SerializedPropertyType.Boolean:
                valueStr = property.boolValue.ToString();
                break;

            case SerializedPropertyType.Float:
                valueStr = property.floatValue.ToString();
                break;

            case SerializedPropertyType.String:
                valueStr = property.stringValue.ToString();
                break;

            default:
                valueStr = "(Not Supported)";
                break;
        }
        EditorGUI.LabelField(position, label.text, valueStr);
    }
}
