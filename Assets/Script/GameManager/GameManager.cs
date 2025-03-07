using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool pauseGame = true;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool GetPause()
    { 
        return pauseGame; 
    }

    public void PauseGame()
    {
        pauseGame = !pauseGame;
    }
}
