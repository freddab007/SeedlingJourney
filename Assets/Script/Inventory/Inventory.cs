using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] int nbLine = 3;
    [SerializeField] int nbColumn = 9;
    Item[][] inventory;

    Item itemMouse;

    public static ItemDataList data;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        data = Resources.Load<ItemDataList>("DataBases/ItemDataBase/ItemDataList");

        inventory = new Item[nbLine][];

        for (int i = 0; i < nbLine; i++)
        {
            inventory[i] = new Item[nbColumn];
        }

        AddItem(data.items[1]);
        AddItem(data.items[2]);
        AddItem(data.items[0]);
        
    }

    public Item GetItem(int _line, int _col)
    {
        return inventory[_line][_col];
    }

    public Item[][] GetInventory()
    {
        return inventory;
    }

    public Vector2Int SearchItem(Item _itemSearch, bool _addInInventory = false)
    {
        Vector2Int invPos = new Vector2Int( -1, -1);

        for (int i = 0; i < inventory.Length; i++)
        {
            for (int j = 0; j < inventory[i].Length; j++)
            {
                if (inventory[i][j].itemId == _itemSearch.itemId)
                {
                    if (_addInInventory && inventory[i][j].nbItem == inventory[i][j].maxNbItem)
                    {
                        invPos.x = -1;
                        invPos.y = -1;
                        continue;
                    }

                    invPos.x = j;
                    invPos.y = i;
                    return invPos;

                }
            }
        }

        return invPos;
    }

    public Item AddItem(Item _newItem)
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
                AddItem(_newItem);
            }


        }

        return _newItem;
    }

    public bool SubItemByPos( Vector2Int _invPos, int _num)
    {
        if (inventory[_invPos.y][_invPos.x].nbItem - _num >= 0)
        {
            inventory[_invPos.y][_invPos.x].nbItem -= _num;
            return true;
        }
        return false;
    }


    public void OpenInventory()
    {
        if (itemMouse == null)
        {
            GameManager.instance.PauseGame();
            UIGameManager.Instance.OpenInventory();
            UIGameManager.Instance.UpdateInventory(inventory);
        }
    }

    public void TakeItem(Vector2Int _pos)
    {
        if (UIGameManager.Instance.GetOpenChest() == null)
        {
            if (itemMouse == null)
            {
                itemMouse = inventory[_pos.y][_pos.x];
                inventory[_pos.y][_pos.x] = null;
            }
            else
            {
                if (inventory[_pos.y][_pos.x] == null)
                {
                    inventory[_pos.y][_pos.x] = itemMouse;
                    itemMouse = null;
                }
                else
                {
                    Item swapItem = inventory[_pos.y][_pos.x];
                    inventory[_pos.y][_pos.x] = itemMouse;
                    itemMouse = swapItem;
                }
            }
            UIGameManager.Instance.UpdateMousItem(itemMouse);
            UIGameManager.Instance.UpdateInventory(inventory);
        }
        else
        {

            inventory[_pos.y][_pos.x] = UIGameManager.Instance.GetOpenChest().AddItem(inventory[_pos.y][_pos.x]);

            if (inventory[_pos.y][_pos.y] != null)
            {
                inventory[_pos.y][_pos.y] = new Item(inventory[_pos.y][_pos.y]);
            }


            UIGameManager.Instance.UpdateInventory(inventory);
            UIGameManager.Instance.UpdateChest(UIGameManager.Instance.GetOpenChest().GetItems(), UIGameManager.Instance.GetOpenChest().GetSize());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
