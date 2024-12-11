using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class AssetsSearchTexture : EditorWindow
{
    Item callBackVariable;
    int listIndex;

    List<Texture2D> TList;
    string search = "";
    Vector2 scrollPosition = Vector2.zero;

    static TypeItem filterItem;

    public delegate void DelegateItemPicker(Item _callbackVariable, int _index, Texture2D _changeVariable);
    DelegateItemPicker callback;

    public delegate void DelegateSpriteInventory(Item _callbackVariable, Texture2D _changeVariable);
    DelegateSpriteInventory callbackSpriteInventory;

    public static AssetsSearchTexture OpenWindow()
    {
        return GetWindow<AssetsSearchTexture>("Searcher");
    }
    public void RegisterCallback(DelegateItemPicker _callback, Item _callBackVariable, int _index)
    {
        TList = new List<Texture2D>();
        GetAtPath();
        callback = _callback;
        callBackVariable = _callBackVariable;
        listIndex = _index;
    }
    public void RegisterCallback(DelegateSpriteInventory _callback, Item _callBackVariable)
    {
        TList = new List<Texture2D>();
        GetAtPath();
        callbackSpriteInventory = _callback;
        callBackVariable = _callBackVariable;
    }

    

    public void GetAtPath()
    {
        foreach (string item in Directory.GetFiles("Assets/Resources/Texture"))
        {
            if (!item.Contains(".meta"))
            {
                TList.Add((Texture2D)EditorGUIUtility.Load(item));
            }
        }
        

    }

    private void OnEnable()
    {
        TList = new List<Texture2D>();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search : ", GUILayout.Width(80));
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();


        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (GUILayout.Button("None"))
        {
            Close();
            if (callback != null)
            {
                callback?.Invoke(callBackVariable, listIndex, null);
            }
            if (callbackSpriteInventory != null)
            {
                callbackSpriteInventory?.Invoke(callBackVariable, null);
            }
        }

        for (int i = 0; i < TList.Count; i++)
        {
            GUILayout.BeginHorizontal();

            Texture2D item = TList[i];
            if (GUILayout.Button(item))
            {
                Close();
                if (callback != null)
                {
                    callback?.Invoke(callBackVariable, listIndex, item);
                }
                if (callbackSpriteInventory != null)
                {
                    callbackSpriteInventory?.Invoke(callBackVariable, item);
                }
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }
}
