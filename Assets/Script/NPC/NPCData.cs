using System.Collections.Generic;

public enum NPCType
{
    SHOPPER,
    DIALOG,
    QUESTGIVER,
    NBTYPE
}

public enum NPCAction
{
    IDLE,
    WALK,
    INROUTINE,
    NBACTION
}

public class NPCData
{
    public int id;

    public string name;

    public List<NPCType> type = new List<NPCType>();

    public List<Routine> routines = new List<Routine>();

    public Dictionary<Routine, List<string>> dialog;
}
