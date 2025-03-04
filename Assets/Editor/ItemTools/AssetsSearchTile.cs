using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


public class AssetsSearchTile : EditorWindow
{
    Item callBackVariable;
    int listIndex;

    List<Tile> TList;
    List<Tile> filteredTile;
    List<Texture2D> filteredTex;
    string search = "";
    string lastsearch;
    Vector2 scrollPosition = Vector2.zero;

    public delegate void DelegateItemPicker(Item _callbackVariable, int _index, Tile _changeVariable);
    DelegateItemPicker callback;

    static GUIStyle buttonStyleTile = new GUIStyle(GUI.skin.button);

    public static AssetsSearchTile OpenWindow()
    {
        buttonStyleTile.alignment = TextAnchor.MiddleLeft; // Alignement à gauche
        buttonStyleTile.imagePosition = ImagePosition.ImageLeft;
        return GetWindow<AssetsSearchTile>("Searcher");
    }
    public void RegisterCallback(DelegateItemPicker _callback, Item _callBackVariable, int _index)
    {
        lastsearch = search;
        TList = new List<Tile>();
        GetAtPath();
        callback = _callback;
        callBackVariable = _callBackVariable;
        listIndex = _index;

        FilterTexByName(search);
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
                Tile temp;
                try
                {
                    temp = (Tile)EditorGUIUtility.Load(file);
                }
                catch (System.Exception)
                {
                    continue;
                    throw;

                }
                if (temp)
                {
                    TList.Add((Tile)EditorGUIUtility.Load(file));
                }
            }
        }


    }

    private void OnEnable()
    {
        TList = new List<Tile>();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Search : ", GUILayout.Width(80));
        search = EditorGUILayout.TextField(search);
        GUILayout.EndHorizontal();

        if (search != lastsearch)
        {
            lastsearch = search;
            FilterTexByName(search);
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (GUILayout.Button("None", buttonStyleTile))
        {
            Close();
            if (callback != null)
            {
                callback?.Invoke(callBackVariable, listIndex, null);
            }
        }

        for (int i = 0; i < filteredTile.Count; i++)
        {
            GUILayout.BeginHorizontal();

            //Tile item = filteredTile[i];
            //Texture2D tex = GetCroppedTexture(item.sprite);

            if (GUILayout.Button(filteredTex[i], buttonStyleTile))
            {
                Close();
                if (callback != null)
                {
                    callback?.Invoke(callBackVariable, listIndex, filteredTile[i]);
                }
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }

    private Texture2D GetCroppedTexture(Sprite sprite)
    {
        if (sprite == null || sprite.texture == null) return null;

        // Vérifie si la texture est lisible
        if (!sprite.texture.isReadable)
        {
            Debug.LogError("La texture du sprite " + sprite.name + " n'est pas lisible. Activez 'Read/Write Enabled' dans les import settings.");
            return null;
        }

        Rect rect = sprite.rect; // Zone du sprite dans la texture complète
        Texture2D originalTexture = sprite.texture;

        // Crée une nouvelle texture vide avec la taille du sprite
        Texture2D croppedTexture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGBA32, false);

        // Copie uniquement les pixels correspondant au sprite
        Color[] pixels = originalTexture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }


    void FilterTexByName(string _search)
    {

        filteredTile = TList.Where(x => x.name.ToLower().Contains(_search.ToLower())).ToList();


        List<Texture2D> tempTexs = new List<Texture2D>();

        for (int i = 0; i < filteredTile.Count; i++)
        {
            tempTexs.Add(GetCroppedTexture(filteredTile[i].sprite));

            filteredTex = tempTexs;
        }
    }
}
