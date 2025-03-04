using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class AssetsSearchWindow<T> : EditorWindow
{
    T callBackVariable;
    List<T> TList;
    string search = "";
    Vector2 scrollPosition = Vector2.zero;

    static TypeItem filterItem;

    public delegate void DelegateItemPicker(T _callbackVariable, T _changeVariable);
    DelegateItemPicker callback;

    public static AssetsSearchWindow<T> OpenWindow()
    {
        return GetWindow<AssetsSearchWindow<T>>("Searcher");
    }
    public void RegisterCallback(DelegateItemPicker _callback, T _callBackVariable)
    {
        GetAtPath();
        callback = _callback;
        callBackVariable = _callBackVariable;
    }

    public void GetAtPath()
    {

        ArrayList al = new ArrayList();
        string[] fileEntries = Directory.GetFiles("Assets/");
        foreach (string fileName in fileEntries)
        {
            int index = fileName.LastIndexOf("/");
            string localPath = "Assets/";

            if (index > 0)
                localPath += fileName.Substring(index);

            Object t = Resources.Load(localPath, typeof(T));

            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];

        TList = result.ToList();
    }

    private void OnEnable()
    {
        //itemList = Resources.Load<ItemDataList>("ItemDataBase/ItemDataList");
        //filter = (TypeItem)(~0);
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search : ", GUILayout.Width(80));
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();

        //FilterList();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < TList.Count; i++)
        {
            GUILayout.BeginHorizontal();

            T item = TList[i];
            if (TList[i] as Texture2D)
            {
                if (GUILayout.Button(TList[i] as Texture2D))
                {
                    Close();
                    callback?.Invoke( callBackVariable, TList[i]);
                }
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Open Item DataBase"))
        {
            Close();
            ItemDataBaseWindow.OpenWindow();
        }
    }
}
