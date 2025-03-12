using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDatabase", menuName = "Seedling Journey/DataBases/NPC DataBase")]

public class NPCDataBase : ScriptableObject
{
    public List<NPCData> NPCDatas = new List<NPCData>();
}
