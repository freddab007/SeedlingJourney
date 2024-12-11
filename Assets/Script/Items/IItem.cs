using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum TypeItem
{
    ARMOR = 0b00000001,
    TOOL = 0b00000010,
    WEAPON = 0b00000100,
    SEED = 0b00001000,
    FOOD = 0b00010000,
    RESOURCE = 0b00100000,
    BUILD = 0b01000000,
}

public enum ArmorType
{
    HELMET,
    BOOTS
}

public enum ToolType
{
    NONE,
    HOE,
    WATERINGCAN,
}

public enum WeaponType
{
    SWORD,
}

public enum SeedType
{
    SEED,
    SAPPLING
}

public enum FoodType
{
    VEGETABLE,
    FRUIT
}

public enum ResourceType
{
    MINERAL,
    MATERIAL,
    FORAGE
}

public enum BuildType
{
    CHEST
}

public interface IItem
{

}
