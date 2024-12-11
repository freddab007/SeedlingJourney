using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ItemDataBaseWindow : EditorWindow
{
    ItemDataList itemList;
    Vector2 scrollPosition = Vector2.zero;
    List<Item> filteredItems = new List<Item>();

    public TypeItem filter;

    Color baseColor;

    string Itemname = "default";

    Texture2D newTex;

    [MenuItem("Tools/Item Database")]

    public static void OpenWindow()
    {
        GetWindow<ItemDataBaseWindow>("Item Database");

    }

    private void OnEnable()
    {
        itemList = Resources.Load<ItemDataList>("ItemDataBase/ItemDataList");
        filter = (TypeItem)(~0);

        newTex = (Texture2D)EditorGUIUtility.Load("Assets/Prefab/BGSprite.png");
    }


    private void PrintLabelInColor(string _text)
    {
        GUI.contentColor = baseColor;

        EditorGUILayout.LabelField(_text);
        GUI.contentColor = Color.green;
    }

    private void PrintLabelInColor(string _text, int _widthLayout)
    {
        GUI.contentColor = baseColor;

        EditorGUILayout.LabelField(_text, GUILayout.Width(_widthLayout));
        GUI.contentColor = Color.green;
    }

    private void DisplayArmor(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.armorType = (ArmorType)EditorGUILayout.EnumPopup(_item.armorType);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        PrintLabelInColor("Def :", 40);

        _item.defense = (int)EditorGUILayout.IntField(_item.defense);

        //GUILayout.EndHorizontal();
    }

    private void DisplayTool(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.toolType = (ToolType)EditorGUILayout.EnumPopup(_item.toolType);

        //GUILayout.EndHorizontal();
    }

    private void DisplayWeapon(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.weaponType = (WeaponType)EditorGUILayout.EnumPopup(_item.weaponType);

        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        PrintLabelInColor("Dam :", 40);
        _item.damage = EditorGUILayout.IntField(_item.damage);
    }

    private void DisplaySeed(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.seedType = (SeedType)EditorGUILayout.EnumPopup(_item.seedType);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        PrintLabelInColor("Sea :", 40);
        _item.seedSeason = (TimeGame.Season)EditorGUILayout.EnumPopup(_item.seedSeason);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        PrintLabelInColor("Col :", 40);
        _item.collision = EditorGUILayout.Toggle(_item.collision);

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (_item.itemGiver != -1)
        {
            if (GUILayout.Button("Give : " + itemList.items[_item.itemGiver].itemName))
            {
                AssetsSearchItem.OpenWindow().RegisterCallback(ChangeItemSeed);
            }
        }
        else
        {
            if (GUILayout.Button("Give : " + "Nothing"))
            {
                AssetsSearchItem.OpenWindow().RegisterCallback(ChangeItemSeed);
            }
        }

        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        PrintLabelInColor("Tim :", 40);
        _item.timeGrowth = EditorGUILayout.IntField(_item.timeGrowth);
        PrintLabelInColor("Num :", 40);
        _item.numberGive = EditorGUILayout.IntField(_item.numberGive);

        //GUILayout.EndHorizontal();
    }

    private void DisplayFood(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.foodType = (FoodType)EditorGUILayout.EnumPopup(_item.foodType);

        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        PrintLabelInColor("Lif :", 40);
        _item.lifeRestore = EditorGUILayout.IntField(_item.lifeRestore);
        PrintLabelInColor("Sta :", 40);
        _item.energyRestore = EditorGUILayout.IntField(_item.energyRestore);

        //GUILayout.EndHorizontal();
    }

    private void DisplayResource(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.resourceType = (ResourceType)EditorGUILayout.EnumPopup(_item.resourceType);

        //GUILayout.EndHorizontal();
    }

    private void DisplayBuild(Item _item)
    {
        GUILayout.BeginHorizontal();

        PrintLabelInColor("Typ :", 40);
        _item.buildType = (BuildType)EditorGUILayout.EnumPopup(_item.buildType);

        //GUILayout.EndHorizontal();
    }

    private void OnGUI()
    {
        baseColor = GUI.color;

        GUILayout.BeginHorizontal();
        PrintLabelInColor("Filter : ", 40);
        filter = (TypeItem)EditorGUILayout.EnumFlagsField(filter);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();

        Itemname = EditorGUILayout.TextField(Itemname);

        GUI.contentColor = Color.green;
        if (GUILayout.Button("New item"))
        {
            Item tempItem = new();
            tempItem.itemId = itemList.items.Count;
            tempItem.itemName = Itemname;
            tempItem.itemType = TypeItem.ARMOR;
            tempItem.texItem = new List<Texture2D>();
            tempItem.inventorySprite = newTex;
            itemList.items.Add(tempItem);
        }
        GUI.contentColor = baseColor;

        GUILayout.EndHorizontal();

        FilterItems();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        for (int i = 0; i < filteredItems.Count; i++)
        {
            GUILayout.BeginHorizontal();

            Item item = filteredItems[i];

            PrintLabelInColor("Id : ", 30);
            EditorGUILayout.LabelField(item.itemId.ToString(), GUILayout.Width(100));

            PrintLabelInColor("Nam :", 40);
            item.itemName = EditorGUILayout.TextField(item.itemName);

            PrintLabelInColor("Typ :", 40);
            item.itemType = (TypeItem)EditorGUILayout.EnumPopup(item.itemType);

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            PrintLabelInColor("Desc :", 40);
            item.itemDescription = EditorGUILayout.TextField(item.itemDescription);

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            PrintLabelInColor("Nb :", 40);
            item.nbItem = EditorGUILayout.IntField(item.nbItem);

            PrintLabelInColor("MaxNb :", 50);
            item.maxNbItem = EditorGUILayout.IntField(item.maxNbItem);


            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            PrintLabelInColor("InvSpri :", 40);
            GUI.contentColor = baseColor;
            if (GUILayout.Button(item.inventorySprite, GUILayout.Width(32), GUILayout.Height(32)))
            {
                AssetsSearchTexture.OpenWindow().RegisterCallback(ChangeTextInventory, item);
            }
            GUI.contentColor = Color.green;

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            PrintLabelInColor("NbTex :", 45);

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                --item.nbOfTex;
                if (item.nbOfTex >= 0)
                {
                    item.texItem.RemoveAt(item.nbOfTex);
                }
                if (item.nbOfTex < 0)
                {
                    item.nbOfTex = 0;
                }
            }

            PrintLabelInColor(item.nbOfTex.ToString(), 20);

            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                ++item.nbOfTex;
                item.texItem.Add(newTex);

            }

            GUILayout.EndHorizontal();

            switch (item.itemType)
            {
                case TypeItem.ARMOR:
                    DisplayArmor(item);
                    break;
                case TypeItem.TOOL:
                    DisplayTool(item);
                    break;
                case TypeItem.WEAPON:
                    DisplayWeapon(item);
                    break;
                case TypeItem.SEED:
                    DisplaySeed(item);
                    break;
                case TypeItem.FOOD:
                    DisplayFood(item);
                    break;
                case TypeItem.RESOURCE:
                    DisplayResource(item);
                    break;
                case TypeItem.BUILD:
                    DisplayBuild(item);
                    break;
                default:
                    break;
            }

            GUILayout.EndHorizontal();


            for (int j = 0; j < item.nbOfTex; j++)
            {
                GUILayout.BeginHorizontal();
                GUI.contentColor = baseColor;
                if (GUILayout.Button(item.texItem[j], GUILayout.Width(32), GUILayout.Height(32)))
                {
                    AssetsSearchTexture.OpenWindow().RegisterCallback(ChangeText, item, j);
                }
                PrintLabelInColor(item.texItem[j].name);
                //GUILayout.Label(item.texItem[j], GUILayout.MaxWidth(32f), GUILayout.MaxHeight(32f));

                GUILayout.BeginVertical();
                if (j > 0)
                {
                    if (GUILayout.Button("Up"))
                    {
                        Texture2D swap = item.texItem[j - 1];
                        item.texItem[j - 1] = item.texItem[j];
                        item.texItem[j] = swap;
                    }
                }
                else
                {
                    PrintLabelInColor("Already First Item");
                }
                if (j < item.nbOfTex - 1)
                {
                    if (GUILayout.Button("Down"))
                    {
                        Texture2D swap = item.texItem[j + 1];
                        item.texItem[j + 1] = item.texItem[j];
                        item.texItem[j] = swap;
                    }
                }
                else
                {
                    PrintLabelInColor("Already Last Item");
                }
                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            GUI.color = Color.red;
            if (GUILayout.Button("X"))
            {
                itemList.items.Remove(item);
                for (int j = 0; j < itemList.items.Count; j++)
                {
                    itemList.items[j].itemId = j;
                }
            }
            GUI.color = baseColor;
            GUILayout.EndHorizontal();


        }





        GUILayout.EndScrollView();

        if (GUILayout.Button("Save"))
        {
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(itemList);
            AssetDatabase.SaveAssets();
        }

    }

    void FilterItems()
    {
        filteredItems = itemList.items.Where(x => ((int)x.itemType & (int)filter) == (int)x.itemType).ToList();
        //filteredItems = itemList.items;
    }


    void ChangeText(Item _tex, int _index, Texture2D _texChoice)
    {
        if (_texChoice == null)
        {
            filteredItems.Where(x => x == _tex).First().texItem.RemoveAt(_index);
            filteredItems.Where(x => x == _tex).First().nbOfTex--;

            return;
        }
        filteredItems.Where(x => x == _tex).First().texItem[_index] = _texChoice;
    }


    void ChangeTextInventory(Item _tex, Texture2D _texChoice)
    {
        if (_texChoice == null)
        {
            filteredItems.Where(x => x == _tex).First().inventorySprite = newTex;
            return;
        }
        filteredItems.Where(x => x == _tex).First().inventorySprite = _texChoice;
    }


    void ChangeItemSeed(int _id, int _itemChoice)
    {
        filteredItems[_id].itemGiver = _itemChoice;
    }
}
