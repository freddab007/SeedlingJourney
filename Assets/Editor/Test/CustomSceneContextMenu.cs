using UnityEngine;
using UnityEditor;
using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using Unity.VisualScripting;

[InitializeOnLoad]
public class CustomSceneContextMenu
{
    static Vector2 lastMousePos = Vector2.zero;
    static Vector2 actualMousePos = Vector2.zero;
    static Vector3 pos = Vector2.zero;
    static bool mouseDown = false;
    static CustomSceneContextMenu()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Camera actualCamera = sceneView.camera;
        
        actualMousePos = Event.current.mousePosition;
        if (Event.current.type == EventType.MouseDown && !mouseDown)
        {
            lastMousePos = actualMousePos;
            mouseDown = true;
        }
        else
        {
            mouseDown = false;
        }

        if (Event.current.IsRightMouseButton() && Event.current.type == EventType.MouseUp && lastMousePos == actualMousePos)
        {
            //Event.current.type == EventType.MouseUp
            if (actualCamera != null)
            {

                ShowContextMenu(actualMousePos);
            }
            else
            {
                Debug.Log("camera is null");
            }
        }
    }

    private static void ShowContextMenu(Vector2 mousePos)
    {
        GenericMenu menu = new GenericMenu();

        // Ajouter une option pour ouvrir un éditeur de code
        pos = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(new Vector3( mousePos.x, mousePos.y, 0), Camera.MonoOrStereoscopicEye.Mono);
        int diffPos = Mathf.FloorToInt(pos.y) - Mathf.FloorToInt(SceneView.currentDrawingSceneView.camera.transform.position.y);
        pos.x = Mathf.FloorToInt(pos.x);
        pos.y = Mathf.FloorToInt(pos.y - diffPos * 2);
        pos.z = 0;
        menu.AddItem(new GUIContent("Seedling Journey/Get Position"), false, OpenCodeEditor);
        menu.ShowAsContext();
    }

    private static void OpenCodeEditor()
    {
        //CustomCodeEditor.ShowWindow();
        ExecuteCode(pos);
        RoutineSearchWindow.OpenWindow().RegisterCallbackPos(pos);
    }

    static public void ExecuteCode(Vector3 _pos)
    {
        Debug.Log("Position on map : " + _pos);
        // Pour exécuter du C# dynamique, il faudrait utiliser Roslyn ou une autre solution.
    }
}