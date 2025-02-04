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
    List<Tile> filteredTex;
    string search = "";
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
        TList = new List<Tile>();
        GetAtPath();
        callback = _callback;
        callBackVariable = _callBackVariable;
        listIndex = _index;
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

        FilterTexByName(search);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (GUILayout.Button("None", buttonStyleTile))
        {
            Close();
            if (callback != null)
            {
                callback?.Invoke(callBackVariable, listIndex, null);
            }
        }

        for (int i = 0; i < filteredTex.Count; i++)
        {
            GUILayout.BeginHorizontal();

            Tile item = filteredTex[i];
            Debug.Log(item.sprite.textureRect);
            Texture2D tex = GetCroppedTexture(item.sprite);

            if (GUILayout.Button(tex, buttonStyleTile))
            {
                Close();
                if (callback != null)
                {
                    callback?.Invoke(callBackVariable, listIndex, item);
                }
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }

    private Texture2D GetCroppedTexture(Sprite sprite)
    {
        if (sprite == null || sprite.texture == null) return null;

        Rect rect = sprite.rect; // Zone du sprite dans la texture complète
        Texture2D originalTexture = sprite.texture;

        // Créer une copie de la texture pour la rendre lisible
        RenderTexture rt = RenderTexture.GetTemporary(originalTexture.width, originalTexture.height);
        Graphics.Blit(originalTexture, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D readableTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.RGBA32, false);
        readableTexture.ReadPixels(new Rect(0, 0, originalTexture.width, originalTexture.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        // Extraire la portion du sprite
        Texture2D croppedTexture = new Texture2D((int)rect.width, (int)rect.height);
        Color[] pixels = readableTexture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        Object.DestroyImmediate(readableTexture); // Nettoyage mémoire

        return croppedTexture;
    }

    void FilterTexByName(string _search)
    {
        filteredTex = TList.Where(x => x.sprite.texture.name.ToLower().Contains(_search.ToLower())).ToList();
    }
}
