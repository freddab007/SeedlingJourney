using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabaseList")]

public class ItemDataList : ScriptableObject
{
    public List<Item> items = new List<Item>();
}
