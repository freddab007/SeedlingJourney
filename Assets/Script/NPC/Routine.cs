using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoutineAction
{
    SELLING,
    LOOKING,
    FISHING,
    SLEEPING,
    NBROUTINE
}

public class Routine
{
    public int idRoutine;

    public string name;

    public Vector3 routinePos = Vector3.zero;

    public RoutineAction action;

    public TimeGame.GameTime startRoutine;
}
