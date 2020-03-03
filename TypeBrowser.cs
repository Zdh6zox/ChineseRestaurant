using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TypeBrowser : EditorWindow
{
    private List<System.Type> _Types = new List<System.Type>();

    public delegate void CreateNew(System.Type type);

    public CreateNew CreateNewFunc;

    public static TypeBrowser ShowBrowser()
    {
        return EditorWindow.GetWindow(typeof(TypeBrowser)) as TypeBrowser;
    }

    public void InitBrowser(System.Type browsingType)
    {
        this.titleContent = new GUIContent(browsingType.Name);

        var types = Assembly.GetCallingAssembly().GetTypes();

        foreach (var type in types)
        {
            var baseType = type.BaseType;
            if (baseType != null)
            {
                if (baseType.Name == browsingType.Name)
                {
                    System.Type objType = System.Type.GetType(type.FullName, true);
                    _Types.Add(objType);
                }
            }
        }
    }

    public void OnGUI()
    {
        GUILayout.BeginVertical();

        foreach (System.Type type in _Types)
        {
            if (GUILayout.Button(type.Name))
            {
                CreateNewFunc(type);
            }
        }
        GUILayout.EndVertical();
    }
}
