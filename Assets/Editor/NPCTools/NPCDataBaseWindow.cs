using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class NPCDataBaseWindow : EditorWindow
{
    string search = "";
    string NPCdefaultName = "Julien Boris";

    Vector2 scrollPosition = Vector2.zero;

    static NPCDataBase NPCDatas;
    RoutineDataBase routineDatas;

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
        routineDatas = Resources.Load<RoutineDataBase>("Databases/Routine/RoutineDataBase");

        baseColor = GUI.color;
    }

    void FilterNPC()
    {
        filteredData = NPCDatas.NPCDatas.Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
    }


    private void DrawCommonVariable(NPCData _nPC)
    {
        GUILayout.BeginHorizontal();

        EditorHelp.PrintLabelInColor("Id: ", baseColor, Color.green, 30);
        EditorGUILayout.LabelField(_nPC.id.ToString(), GUILayout.Width(100));

        EditorHelp.PrintLabelInColor("Nam: ", baseColor, Color.green, 30);
        _nPC.name = EditorGUILayout.TextField(_nPC.name);

        GUILayout.EndHorizontal();
    }

    private void DrawTypeNPC(NPCData _npc)
    {
        if (_npc.type.Count > 0)
        {
            GUILayout.BeginHorizontal();
            EditorHelp.PrintLabelInColor("NPC Types: ", baseColor, Color.green, 100);
            GUILayout.EndHorizontal();

            for (int i = 0; i < _npc.type.Count; i++)
            {
                GUILayout.BeginHorizontal();

                //_npc.type[i] = (NPCType)EditorGUILayout.EnumPopup(_npc.type[i]);
                NPCType nPCType = _npc.type[i];
                EditorHelp.ShowFilteredEnum(new NPCType[] { NPCType.NBTYPE }, ref nPCType);
                _npc.type[i] = nPCType;

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
                    EditorHelp.PrintLabelInColor("Already First Type", baseColor, Color.green, true);
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
                    EditorHelp.PrintLabelInColor("Already Last Type", baseColor, Color.green, true);
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


    void AddRemTextToDialog(List<string> _dialog)
    {
        if (_dialog.Count > 0 && GUILayout.Button("-", GUILayout.Width(20)))
        {
            _dialog.RemoveAt(_dialog.Count - 1);
        }

        EditorHelp.PrintLabelInColor(_dialog.Count.ToString(), baseColor, Color.green, 10);

        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            _dialog.Add("");
        }
    }

    void DrawDialog(NPCData _npc, Routine _routine)
    {
        GUILayout.BeginHorizontal();
        EditorHelp.PrintLabelInColor(_routine.name + ": ", baseColor, Color.green);
        AddRemTextToDialog(_npc.dialog[_routine]);
        GUILayout.EndHorizontal();

        for (int j = 0; j < _npc.dialog[_routine].Count; j++)
        {
            GUILayout.BeginHorizontal();
            string dialog = _npc.dialog[_routine][j];
            EditorHelp.PrintLabelInColor(j.ToString() + ": ", baseColor, Color.green, 15);
            dialog = EditorGUILayout.TextField(dialog);
            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                _npc.dialog[_routine].RemoveAt(j);
            }
            GUILayout.EndHorizontal();
        }

    }


    private void AddRemoveType(NPCData _npc)
    {
        GUILayout.BeginHorizontal();

        EditorHelp.PrintLabelInColor("NbType: ", baseColor, Color.green, 45);

        if (_npc.type.Count > 0)
        {
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                _npc.type.RemoveAt(_npc.type.Count - 1);
            }
        }

        EditorHelp.PrintLabelInColor(_npc.type.Count.ToString(), baseColor, Color.green, 20);

        if (_npc.type.Count < (int)NPCType.NBTYPE)
        {
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                _npc.type.Add(NPCType.DIALOG);

            }
        }

        GUILayout.EndHorizontal();
    }


    private void DrawRoutine(NPCData _npc)
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Routine"))
        {
            RoutineSearchWindow.OpenWindow().RegisterCallback(AddRoutine, _npc, ref _npc.routines);
        }
        GUILayout.EndHorizontal();

        if (routineDatas.routines.Count == 0)
        {
            _npc.routines.Clear();
        }
        for (int i = 0; i < _npc.routines.Count; i++)
        {
            Routine routine = routineDatas.routines[_npc.routines[i].idRoutine];

            GUILayout.BeginHorizontal();
            EditorHelp.PrintLabelInColor("Id: ", baseColor, Color.green, 30);

            EditorHelp.PrintLabelInColor(routine.idRoutine.ToString(), baseColor, Color.green);

            EditorHelp.PrintLabelInColor("Name: ", baseColor, Color.green, 40);
            EditorHelp.PrintLabelInColor(routine.name, baseColor, Color.green);

            if (GUILayout.Button("X", GUILayout.Width(20), GUILayout.Height(20)))
            {
                _npc.routines.Remove(routine);
                _npc.dialog.Remove(routine);
                routine = null;
            }
            GUILayout.EndHorizontal();

            if (routine != null)
            {
                DrawDialog(_npc, routine);
            }
        }

    }

    public static void DeleteARoutine(int _index)
    {
        NPCDatas = Resources.Load<NPCDataBase>("Databases/NPCDataBase/NPCDataBase");

        List<NPCData> NPCs = NPCDatas.NPCDatas.Where(x => x.routines.Where(y => y.idRoutine == _index) != null).ToList();
        for (int i = 0; i < NPCs.Count; i++)
        {
            for (int j = 0; j < NPCs[i].routines.Count; j++)
            {
                if (NPCs[i].routines[j].idRoutine == _index)
                {
                    NPCs[i].routines.RemoveAt(j);
                }
            }
        }
    }

    public void AddRoutine(NPCData _npc, int _routineIndex)
    {
        if (_routineIndex >= 0)
        {
            NPCDatas.NPCDatas.Where(x => x == _npc).First()?.routines.Add(routineDatas.routines[_routineIndex]);
            NPCDatas.NPCDatas.Where(x => x == _npc).First()?.dialog.Add(routineDatas.routines[_routineIndex], new List<string>());
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        EditorHelp.PrintLabelInColor("FilterName: ", baseColor, Color.green, 70);
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

            DrawRoutine(npc);

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
