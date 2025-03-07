using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RoutineSearchWindow : EditorWindow
{
    NPCData callBackVariable;

    RoutineDataBase listData;
    string search = "";
    Vector2 scrollPosition = Vector2.zero;

    public delegate void DelegateRoutineAdding(NPCData _callbackVariable, int _changeVariable);
    DelegateRoutineAdding callback;

    List<Routine> actualRoutine = new List<Routine>();
    List<Routine> firstFilteredRoutine = new List<Routine>();
    List<Routine> filteredRoutine = new List<Routine>();

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

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search : ", GUILayout.Width(80));
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();

        FilterBySearch();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < filteredRoutine.Count; i++)
        {
            GUILayout.BeginHorizontal();

            Routine routine = filteredRoutine[i];
            if (GUILayout.Button(routine.name))
            {
                Close();

                callback?.Invoke(callBackVariable, routine.idRoutine);
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }

}
