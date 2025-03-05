using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDatabase")]

public class NPCDataBase : ScriptableObject
{
    public List<NPCData> NPCDatas = new List<NPCData>();
}
