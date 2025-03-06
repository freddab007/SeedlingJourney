using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RoutineDataBase", menuName = "Seedling Journey/DataBases/Routine DataBase")]

public class RoutineDataBase : ScriptableObject
{
    public List<Routine> routines = new List<Routine>();
}
