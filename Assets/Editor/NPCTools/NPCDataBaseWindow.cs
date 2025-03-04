using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCDataBaseWindow : EditorWindow
{
    string search = "";
    string NPCdefaultName = "Julien Boris";

    NPCDataBase NPCDatas;

    Color baseColor;

    [MenuItem("Seedling Journey/NPC DataBase")]
    public static void OpenWindow()
    {
        GetWindow<NPCDataBaseWindow>("NPC DataBase");
    }

    private void OnEnable()
    {
        NPCDatas = Resources.Load<NPCDataBase>("NPCDataBase/NPCDataBase");

        baseColor = GUI.color;
    }

    private void PrintLabelInColor(string _text)
    {
        GUI.contentColor = baseColor;

        EditorGUILayout.LabelField(_text);
        GUI.contentColor = Color.green;
    }

    private void PrintLabelInColor(string _text, int _widthLayout)
    {
        GUI.contentColor = baseColor;

        EditorGUILayout.LabelField(_text, GUILayout.Width(_widthLayout));
        GUI.contentColor = Color.green;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        PrintLabelInColor( "FilterName : ", 70);
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();
    }
}
