using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NPCDataBaseWindow : EditorWindow
{
    string search = "";
    string NPCdefaultName = "Julien Boris";

    Vector2 scrollPosition = Vector2.zero;

    NPCDataBase NPCDatas;

    Color baseColor;

    List<NPCData> filteredData = new List<NPCData>();

    [MenuItem("Seedling Journey/DataBases/NPC DataBase")]
    public static void OpenWindow()
    {
        GetWindow<NPCDataBaseWindow>("NPC DataBase");
    }

    private void OnEnable()
    {
        NPCDatas = Resources.Load<NPCDataBase>("Databases/NPCDataBase/NPCDataBase");

        baseColor = GUI.color;
    }

    void FilterNPC()
    {
        filteredData = NPCDatas.NPCDatas.Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
    }

    private void PrintLabelInColor(string _text, bool _center = false)
    {
        GUI.contentColor = baseColor;

        if (_center)
        {
            GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
            centeredStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.LabelField(_text, centeredStyle, GUILayout.ExpandWidth(true));
        }
        else
        {
            EditorGUILayout.LabelField(_text);
        }
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

        PrintLabelInColor("Nam:", 30);
        _nPC.name = EditorGUILayout.TextField(_nPC.name);

        GUILayout.EndHorizontal();
    }

    private void DrawTypeNPC(NPCData _npc)
    {
        if (_npc.type.Count > 0)
        {
            GUILayout.BeginHorizontal();
            PrintLabelInColor("NPC Types :", 100);
            GUILayout.EndHorizontal();

            for (int i = 0; i < _npc.type.Count; i++)
            {
                GUILayout.BeginHorizontal();

                _npc.type[i] = (NPCType)EditorGUILayout.EnumPopup(_npc.type[i]);

                GUILayout.BeginVertical();

                if (i > 0)
                {
                    if (GUILayout.Button("Up"))
                    {
                        NPCType swapType = _npc.type[i - 1];
                        _npc.type[i - 1] = _npc.type[i];
                        _npc.type[i] = swapType;
                    }
                }
                else
                {
                    PrintLabelInColor("Already First Type", true);
                }

                if (i < _npc.type.Count - 1)
                {
                    if (GUILayout.Button("Down"))
                    {
                        NPCType swapType = _npc.type[i + 1];
                        _npc.type[i + 1] = _npc.type[i];
                        _npc.type[i] = swapType;
                    }
                }
                else
                {
                    PrintLabelInColor("Already Last Type", true);
                }

                GUILayout.EndVertical();

                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(40)))
                {
                    _npc.type.RemoveAt(i);
                    i = 0;
                }

                GUILayout.EndHorizontal();
            }
        }
    }

    private void DrawActionNPC(NPCData _npc)
    {
        if (_npc.actions.Count > 0)
        {
            GUILayout.BeginHorizontal();
            PrintLabelInColor("NPC Actions :", 100);
            GUILayout.EndHorizontal();

            for (int i = 0; i < _npc.actions.Count; i++)
            {
                GUILayout.BeginHorizontal();

                _npc.actions[i] = (NPCAction)EditorGUILayout.EnumPopup(_npc.actions[i]);

                GUILayout.BeginVertical();

                if (i > 0)
                {
                    if (GUILayout.Button("Up"))
                    {
                        NPCAction swapType = _npc.actions[i - 1];
                        _npc.actions[i - 1] = _npc.actions[i];
                        _npc.actions[i] = swapType;
                    }
                }
                else
                {
                    PrintLabelInColor("Already First Action", true);
                }

                if (i < _npc.actions.Count - 1)
                {
                    if (GUILayout.Button("Down"))
                    {
                        NPCAction swapType = _npc.actions[i + 1];
                        _npc.actions[i + 1] = _npc.actions[i];
                        _npc.actions[i] = swapType;
                    }
                }
                else
                {
                    PrintLabelInColor("Already Last Action", true);
                }

                GUILayout.EndVertical();

                if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(40)))
                {
                    _npc.actions.RemoveAt(i);
                    i = 0;
                }

                GUILayout.EndHorizontal();
            }
        }
    }


    void DrawDialog(NPCData _npc)
    {
        GUILayout.BeginHorizontal();

        for (int i = 0; i < _npc.dialog.Count; i++)
        {
            //PrintLabelInColor("Dialog Action : " + _npc.dialog.);
        }

        GUILayout.EndHorizontal();
    }


    private void AddRemoveType(NPCData _npc)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("NbType :", 45);

        if (_npc.type.Count > 0)
        {
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                _npc.type.RemoveAt(_npc.type.Count - 1);
            }
        }

        PrintLabelInColor(_npc.type.Count.ToString(), 20);

        if (_npc.type.Count < (int)NPCType.NBTYPE)
        {
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _npc.type.Add(NPCType.DIALOG);

            }
        }

        GUILayout.EndHorizontal();
    }


    private void AddRemoveAction(NPCData _npc)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("NbAction :", 45);

        if (_npc.actions.Count > 0)
        {
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                _npc.actions.RemoveAt(_npc.actions.Count - 1);
            }
        }

        PrintLabelInColor(_npc.actions.Count.ToString(), 20);

        if (_npc.actions.Count < (int)NPCAction.NBACTION)
        {
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _npc.actions.Add(NPCAction.IDLE);

            }
        }

        GUILayout.EndHorizontal();
    }


    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        PrintLabelInColor("FilterName : ", 70);
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();

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

        FilterNPC();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < filteredData.Count; i++)
        {
            NPCData npc = filteredData[i];

            DrawCommonVariable(npc);

            AddRemoveType(npc);

            DrawTypeNPC(npc);

            AddRemoveAction(npc);

            DrawActionNPC(npc);

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
