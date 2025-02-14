using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]

public class Item : IItem
{

    public TypeItem itemType;


    //Common Stats
    public int itemId;
    public string itemName;

    public string itemDescription;

    public int nbOfTex;
    public int nbOfTile;

    public Texture2D inventorySprite;

    public List<Texture2D> texItem;

    public List<Tile> tileItem;
    public List<Texture2D> tileTex;

    public int maxNbItem = 1;
    public int nbItem = 1;


    //Weapon Stats

    public WeaponType weaponType;

    public int damage;


    //ArmorType

    public ArmorType armorType;

    public int defense;


    //ToolsStats

    public ToolType toolType;


    //SeedStats

    public SeedType seedType;
    public TimeGame.Season seedSeason;

    public int growDay = 0;
    public int timeGrowth; //Day
    public int numberGive; //Number of fruit/vegetable on the harvest

    public int itemGiver = -1; //Which item give the seed at the end
    public bool collision;


    //FoodStats

    public FoodType foodType;

    public int lifeRestore;
    public int energyRestore;


    //RessourceStats

    public ResourceType resourceType;


    //BuildStats

    public BuildType buildType;

    public Item()
    {

    }

    public Item(Item _item)
    {
        itemType = _item.itemType;
        itemId = _item.itemId;
        itemName = _item.itemName;
        itemDescription = _item.itemDescription;
        nbOfTex = _item.nbOfTex;
        nbOfTile = _item.nbOfTile;
        inventorySprite = _item.inventorySprite;
        texItem = _item.texItem;
        tileItem = _item.tileItem;
        maxNbItem = _item.maxNbItem;
        nbItem = _item.nbItem;
        weaponType = _item.weaponType;
        damage = _item.damage;
        armorType = _item.armorType;
        defense = _item.defense;
        toolType = _item.toolType;
        seedType = _item.seedType;
        seedSeason = _item.seedSeason;
        growDay = _item.growDay;
        timeGrowth = _item.timeGrowth; //Day
        numberGive = _item.numberGive; //Number of fruit/vegetable on the harvest
        itemGiver = _item.itemGiver; //Which item give the seed at the end
        collision = _item.collision; 
        foodType = _item.foodType;
        lifeRestore = _item.lifeRestore;
        energyRestore = _item.energyRestore;
        resourceType = _item.resourceType;
    }




}
