using UnityEditor;
using UnityEngine;


public class AssetsSearchItem : EditorWindow
{
    int callBackVariable;

    ItemDataList listData;
    string search = "";
    Vector2 scrollPosition = Vector2.zero;

    static TypeItem filterItem;

    public delegate void DelegateItemSeed(int _callbackVariable, int _changeVariable);
    DelegateItemSeed callback;

    public static AssetsSearchItem OpenWindow()
    {
        return GetWindow<AssetsSearchItem>("Item Searcher");
    }
    public void RegisterCallback(DelegateItemSeed _callback)
    {
        callback = _callback;
    }

    private void OnEnable()
    {
        listData = Resources.Load<ItemDataList>("DataBases/ItemDataBase/ItemDataList");
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

            callback?.Invoke(callBackVariable, -1);
        }

        for (int i = 0; i < listData.items.Count; i++)
        {
            GUILayout.BeginHorizontal();

            Item item = listData.items[i];
            if (GUILayout.Button(item.itemName))
            {
                Close();

                callback?.Invoke(callBackVariable, item.itemId);
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }
}
