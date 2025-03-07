using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class AssetsSearchTexture : EditorWindow
{
    Item callBackVariable;
    int listIndex;

    List<Texture2D> TList;
    List<Texture2D> filteredTex;
    string search = "";
    Vector2 scrollPosition = Vector2.zero;

    public delegate void DelegateItemPicker(Item _callbackVariable, int _index, Texture2D _changeVariable);
    DelegateItemPicker callback;

    public delegate void DelegateSpriteInventory(Item _callbackVariable, Texture2D _changeVariable);
    DelegateSpriteInventory callbackSpriteInventory;

    static GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

    public static AssetsSearchTexture OpenWindow()
    {
        buttonStyle.alignment = TextAnchor.MiddleLeft; // Alignement à gauche
        buttonStyle.imagePosition = ImagePosition.ImageLeft;
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
        //foreach (string item in Directory.GetFiles("Assets/Resources/Texture"))
        //{
        //    {
        //        var temp = (Texture2D)EditorGUIUtility.Load(item);
        //        if (temp)
        //        {
        //            Debug.Log("Good File");
        //        }
        //        else
        //        {
        //            Debug.Log(item);
        //        }
        //    }
        //    if (!item.Contains(".meta"))
        //    {
        //        TList.Add((Texture2D)EditorGUIUtility.Load(item));
        //    }
        //}

        foreach (string file in System.IO.Directory.GetFiles("Assets", "*.*", SearchOption.AllDirectories))
        {
            // Exclure les fichiers qui se trouvent dans un dossier "Editor"
            if (!file.Contains(Path.DirectorySeparatorChar + "Editor" + Path.DirectorySeparatorChar) && !file.Contains(Path.DirectorySeparatorChar + "TextMesh"))
            {
                Texture2D temp;
                try
                {
                    temp = (Texture2D)EditorGUIUtility.Load(file);
                }
                catch (System.Exception)
                {
                    continue;
                    throw;

                }
                if (temp)
                {
                    TList.Add((Texture2D)EditorGUIUtility.Load(file));
                }
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

        FilterTexByName(search);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (GUILayout.Button("None", buttonStyle))
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

        for (int i = 0; i < filteredTex.Count; i++)
        {
            GUILayout.BeginHorizontal();

            Texture2D item = filteredTex[i];
            if (GUILayout.Button(item, buttonStyle))
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

    void FilterTexByName(string _search)
    {
        filteredTex = TList.Where(x => x.name.ToLower().Contains(_search.ToLower())).ToList();
    }
}
