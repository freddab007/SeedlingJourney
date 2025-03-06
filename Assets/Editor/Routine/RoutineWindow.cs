using UnityEditor;
using UnityEngine;

public class RoutineWindow : EditorWindow
{
    string search = "";

    string routinedefaultName = "Selling";

    Vector2 scrollPosition = Vector2.zero;

    Color baseColor;

    RoutineDataBase routines;

    [MenuItem("Seedling Journey/DataBases/Routine DataBase")]
    public static void OpenWindow()
    {
        GetWindow<RoutineWindow>("Routine Database");
    }

    private void OnEnable()
    {
        routines = Resources.Load<RoutineDataBase>("Databases/Routine/RoutineDataBase");

        baseColor = GUI.color;
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

    private void DrawRoutineCommon(Routine _routine)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Id: ", 20);
        PrintLabelInColor(_routine.idRoutine.ToString());

        PrintLabelInColor("Name: ", 40);
        _routine.name = EditorGUILayout.TextField(_routine.name);

        GUILayout.EndHorizontal();
    }

    private void OnGUI()
    {
        baseColor = GUI.color;

        GUILayout.BeginHorizontal();

        routinedefaultName = EditorGUILayout.TextField(routinedefaultName);

        if (GUILayout.Button("New"))
        {
            Routine routine = new Routine();
            routine.idRoutine = routines.routines.Count;
            routine.name = routinedefaultName;
            routines.routines.Add(routine);
        }

        GUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < routines.routines.Count; i++)
        {
            Routine tempRoutine = routines.routines[i];

            DrawRoutineCommon(tempRoutine);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("X"))
            {
                routines.routines.RemoveAt(i);
                for (int j = 0; j < routines.routines.Count; j++)
                {
                    routines.routines[j].idRoutine = j;
                }
                i = 0;
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("SAVE"))
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(routines);
            AssetDatabase.SaveAssets();
        }

        GUILayout.EndHorizontal();
    }
}
