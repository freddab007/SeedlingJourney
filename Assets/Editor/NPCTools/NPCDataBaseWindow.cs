using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCDataBaseWindow : EditorWindow
{
    string search = "";
    string NPCdefaultName = "Julien Boris";

    Vector2 scrollPosition = Vector2.zero;

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


    private void DrawCommonVariable(NPCData _nPC)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Id : ", 30);
        EditorGUILayout.LabelField(_nPC.id.ToString(), GUILayout.Width(100));

        PrintLabelInColor( "Nam:", 30);
        _nPC.name = EditorGUILayout.TextField(_nPC.name);

        GUILayout.EndHorizontal();
    }


    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        PrintLabelInColor( "FilterName : ", 70);
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();


        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.BeginHorizontal();

        NPCdefaultName = EditorGUILayout.TextField(NPCdefaultName);

        GUI.contentColor = Color.green;
        if (GUILayout.Button("New NPC"))
        {
            NPCData tempNPC = new();
            tempNPC.id = NPCDatas.NPCDatas.Count;
            tempNPC.name = NPCdefaultName;
            NPCDatas.NPCDatas.Add(tempNPC);
        }
        GUI.contentColor = baseColor;

        GUILayout.EndHorizontal();



        for (int i = 0; i < NPCDatas.NPCDatas.Count; i++)
        {
            NPCData npc = NPCDatas.NPCDatas[i];

            DrawCommonVariable(npc);


            GUILayout.BeginHorizontal();
            GUI.color = Color.red;
            if (GUILayout.Button("X"))
            {
                NPCDatas.NPCDatas.Remove(npc);
                for (int j = 0; j < NPCDatas.NPCDatas.Count; j++)
                {
                    NPCDatas.NPCDatas[j].id = j;
                }
            }
            GUI.color = baseColor;
            GUILayout.EndHorizontal();
        }


        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();


        if (GUILayout.Button("SAVE"))
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(NPCDatas);
            AssetDatabase.SaveAssets();
        }

        GUILayout.EndHorizontal();
    }
}
