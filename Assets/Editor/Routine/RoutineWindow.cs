using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RoutineWindow : EditorWindow
{
    string search = "";

    string routinedefaultName = "Selling";

    Vector2 scrollPosition = Vector2.zero;

    Color baseColor;

    RoutineDataBase routines;

    List<Routine> filteredRoutine = new List<Routine>();

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

    void FilterRoutine()
    {
        filteredRoutine = routines.routines.Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
    }

    private void DrawTime(Routine _routine)
    {
        EditorHelp.PrintLabelInColor( "WeekDay: ", baseColor, Color.green, 70);
        _routine.startRoutine.weekDay = (TimeGame.Day)EditorGUILayout.EnumPopup(_routine.startRoutine.weekDay);

        EditorHelp.PrintLabelInColor( "Day: ", baseColor, Color.green, 30);
        _routine.startRoutine.day = EditorGUILayout.IntField( _routine.startRoutine.day);

        EditorHelp.PrintLabelInColor( "Hours: ", baseColor, Color.green, 40);
        _routine.startRoutine.hours = EditorGUILayout.IntField( _routine.startRoutine.hours);

        EditorHelp.PrintLabelInColor( "Minute: ", baseColor, Color.green, 50);
        _routine.startRoutine.minute = EditorGUILayout.IntField( _routine.startRoutine.minute);

        EditorHelp.PrintLabelInColor( "Year: ", baseColor, Color.green, 40);
        _routine.startRoutine.year = EditorGUILayout.IntField( _routine.startRoutine.year);
    }

    private void DrawRoutineCommon(Routine _routine)
    {
        GUILayout.BeginHorizontal();
        
        EditorHelp.PrintLabelInColor("Id: ", baseColor, Color.green, 20);

        EditorHelp.PrintLabelInColor(_routine.idRoutine.ToString(), baseColor, Color.green);
        
        EditorHelp.PrintLabelInColor("Name: ", baseColor, Color.green, 40);

        _routine.name = EditorGUILayout.TextField(_routine.name);

        EditorHelp.PrintLabelInColor("Action: ", baseColor, Color.green, 40);

        EditorHelp.ShowFilteredEnum( new RoutineAction[]{ RoutineAction.NBROUTINE}, ref _routine.action);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        
        EditorHelp.PrintLabelInColor("Pos: ", baseColor, Color.green, 30);

        string posString = "x: " + _routine.routinePos.x.ToString() + " y: " + _routine.routinePos.y.ToString() + " z: " + _routine.routinePos.z.ToString();

        EditorHelp.PrintLabelInColor( posString, baseColor, Color.green, true);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        DrawTime(_routine);

        GUILayout.EndHorizontal();
    }

    private void OnGUI()
    {
        baseColor = GUI.color;

        GUILayout.BeginHorizontal();

        EditorHelp.PrintLabelInColor( "search: ", baseColor, Color.green, 45);
        search = EditorGUILayout.TextField(search);

        GUILayout.EndHorizontal();

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

        FilterRoutine();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < filteredRoutine.Count; i++)
        {
            Routine tempRoutine = filteredRoutine[i];

            DrawRoutineCommon(tempRoutine);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("X"))
            {
                routines.routines.Remove(tempRoutine);
                for (int j = 0; j < routines.routines.Count; j++)
                {
                    routines.routines[j].idRoutine = j;
                }
                NPCDataBaseWindow.DeleteARoutine(i);
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
