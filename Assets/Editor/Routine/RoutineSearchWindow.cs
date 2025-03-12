using PlasticGui.WorkspaceWindow.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RoutineSearchWindow : EditorWindow
{
    enum TypeSearch
    {
        SETROUTINEPOS,
        SETROUTINENPC
    }

    NPCData callBackVariable;
    Vector3 callBackPos;

    RoutineDataBase listData;
    string search = "";
    Vector2 scrollPosition = Vector2.zero;

    public delegate void DelegateRoutineAdding(NPCData _callbackVariable, int _changeVariable);
    DelegateRoutineAdding callback;

    List<Routine> actualRoutine = new List<Routine>();
    List<Routine> firstFilteredRoutine = new List<Routine>();
    List<Routine> filteredRoutine = new List<Routine>();

    TypeSearch type = TypeSearch.SETROUTINENPC;

    public static RoutineSearchWindow OpenWindow()
    {
        return GetWindow<RoutineSearchWindow>("Routine Searcher");
    }
    public void RegisterCallback(DelegateRoutineAdding _callback, NPCData _callbackNPC, ref List<Routine> _routines)
    {
        callback = _callback;
        callBackVariable = _callbackNPC;
        actualRoutine = _routines;
        FirstFilter();
        type = TypeSearch.SETROUTINENPC;
    }
    public void RegisterCallbackPos(Vector3 _pos)
    {
        callBackPos = _pos;
        type = TypeSearch.SETROUTINEPOS;
        firstFilteredRoutine = listData.routines;
    }



    private void OnEnable()
    {
        listData = Resources.Load<RoutineDataBase>("DataBases/Routine/RoutineDataBase");
    }

    private void FirstFilter()
    {
        firstFilteredRoutine.Clear();
        for (int i = 0; i < listData.routines.Count; i++)
        {
            List<Routine> tempRoutine = /*null;*/actualRoutine.Where(x => x == listData.routines[i]).ToList();
            if (tempRoutine.Count > 0)
            {
                continue;
            }
            firstFilteredRoutine.Add(listData.routines[i]);
        }
    }

    private void FilterBySearch()
    {
        filteredRoutine = firstFilteredRoutine.Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
    }

    private void SetPosRoutine(Routine _routine)
    {
        _routine.routinePos = callBackPos;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search : ", GUILayout.Width(80));
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();

        FilterBySearch();


        if (filteredRoutine.Count == 0)
        {
            EditorHelp.PrintLabelInColor("No Routine ! Verify your dataBase or empty search", GUI.color, GUI.color, true);
        }
        else
        {
            if (type == TypeSearch.SETROUTINEPOS)
            {
                GUILayout.BeginHorizontal();
                EditorHelp.PrintLabelInColor("Pos Selected : " + callBackPos.ToString(), GUI.color, GUI.color, true);
                GUILayout.EndHorizontal();
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < filteredRoutine.Count; i++)
            {
                GUILayout.BeginHorizontal();

                Routine routine = filteredRoutine[i];
                if (GUILayout.Button(routine.name))
                {
                    Close();

                    if (type == TypeSearch.SETROUTINENPC)
                    {
                        callback?.Invoke(callBackVariable, routine.idRoutine);
                    }
                    else if (type == TypeSearch.SETROUTINEPOS)
                    {
                        SetPosRoutine(routine);
                    }
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }


    }

}
