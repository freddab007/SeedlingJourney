using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int nbLine = 3;
    [SerializeField] int nbColumn = 9;
    Item[][] inventory;

    public static ItemDataList data;

    // Start is called before the first frame update
    void Start()
    {
        data = Resources.Load<ItemDataList>("ItemDataBase/ItemDataList");

        inventory = new Item[nbLine][];

        for (int i = 0; i < nbLine; i++)
        {
            inventory[i] = new Item[nbColumn];
        }

        AddItem(data.items[1]);
        AddItem(data.items[2]);
        AddItem(data.items[0]);

        //for (int i = 0; i < nbLine; i++)
        //{
        //    for (int j = 0; j < nbColumn; j++)
        //    {
        //        if (inventory[i][j] == null)
        //        {
        //            Debug.Log(j.ToString() + "/" + i.ToString() + " = null");
        //        }
        //        else
        //        {
        //            Debug.Log(j.ToString() + "/" + i.ToString() + " = " + inventory[i][j].itemName + " | nb : " + inventory[i][j].nbItem);
        //        }
        //    }
        //}
    }

    public Item GetItem(int _line, int _col)
    {
        return inventory[_line][_col];
    }

    public void AddItem(Item _newItem)
    {
        if (_newItem != null)
        {
            bool itemFind = false;

            for (int i = 0; i < nbLine; i++)
            {
                for (int j = 0; j < nbColumn; j++)
                {
                    if (inventory[i][j] != null && inventory[i][j].itemId == _newItem.itemId)
                    {
                        if (inventory[i][j].nbItem + _newItem.nbItem < inventory[i][j].maxNbItem)
                        {
                            inventory[i][j].nbItem += _newItem.nbItem;
                            _newItem = null;
                            itemFind = true;
                        }
                        else
                        {
                            int left = (inventory[i][j].nbItem + _newItem.nbItem) - inventory[i][j].maxNbItem;
                            inventory[i][j].nbItem = inventory[i][j].maxNbItem;
                            _newItem.nbItem = left;
                        }
                    }
                }

            }
            if (itemFind == false)
            {
                for (int i = 0; i < nbLine; i++)
                {
                    if (itemFind == false)
                    {
                        for (int j = 0; j < nbColumn; j++)
                        {

                            if (itemFind != false)
                            {
                                continue;
                            }
                            if (inventory[i][j] == null)
                            {
                                if (_newItem.nbItem <= _newItem.maxNbItem)
                                {
                                    inventory[i][j] = new Item(_newItem);
                                    _newItem = null;
                                    itemFind = true;
                                }
                                else
                                {
                                    Item tempItem = new Item(_newItem);
                                    tempItem.nbItem = tempItem.maxNbItem;
                                    _newItem.nbItem -= tempItem.maxNbItem;
                                    inventory[i][j] = new Item(tempItem);
                                }
                            }
                        }
                    }
                }
            }
            if (_newItem != null && itemFind)
            {
                AddItem( _newItem );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
