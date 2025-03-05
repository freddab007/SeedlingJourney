using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    SHOPPER,
    DIALOG,
    QUESTGIVER,
    NBTYPE
}

public enum NPCAction
{
    WALK,
    IDLE,
    SHOPPING,
    SELLING,
    HISTORY,
    NBACTION
}

public class NPCData
{
    public int id;

    public string name;

    public List<NPCType> type;
    public List<NPCAction> actions;

    public Dictionary<NPCAction, List<string>> dialog;
}
