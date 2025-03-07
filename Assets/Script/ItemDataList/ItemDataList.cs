using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabaseList", menuName = "Seedling Journey/DataBases/Item DataBase")]

public class ItemDataList : ScriptableObject
{
    public List<Item> items = new List<Item>();
}
