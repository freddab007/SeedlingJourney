using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    static public PlantManager Instance;
    List<Seed> seeds = new List<Seed>();
    List<Seed> testSeed = new List<Seed>();
    Action plantGrow;
    Action plantDeath;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Seed GetSeedPos(Vector2Int _pos)
    {
        testSeed = seeds.Where(x => x.GetPos() == _pos).ToList();
        if (testSeed.Count == 0)
        {
            return null;
        }
        
        return testSeed.First();
    }

    public void AddSeed(Seed _seed)
    {
        seeds.Add(_seed);
    }

    public void SubSeed(Seed _seed)
    {
        seeds.Remove(_seed);
    }

    public void AddPlant(Action _seed)
    {
        plantGrow += _seed;
    }


    public void SubPlant(Action _seed)
    {
        plantGrow -= _seed;
    }


    public void LaunchGrow()
    {
        plantGrow?.Invoke();
    }


    public void AddDeath(Action _seed)
    {
        plantDeath += _seed;
    }


    public void LaunchDeath()
    {
        plantDeath?.Invoke();
        plantDeath = null;
    }
}
