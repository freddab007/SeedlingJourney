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

    public List<NPCType> type = new List<NPCType>();
    public List<NPCAction> actions = new List<NPCAction>();

    public List<Routine> routines = new List<Routine>();

    public Dictionary<Routine, List<string>> dialog;
}
