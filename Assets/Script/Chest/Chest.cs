using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]Vector2Int sizeChest = new Vector2Int( 9, 3);
    Item[,] itemList = new Item[ 3, 9];
    Inventory playerInventory;

    ItemDataList temp;

    // Start is called before the first frame update
    void Start()
    {
        temp = Resources.Load<ItemDataList>("DataBases/ItemDataBase/ItemDataList");
        itemList[ 0, 0] = new Item(temp.items[2]);
        playerInventory = FindAnyObjectByType<Inventory>();
    }


    public void OpenChest()
    {
        UIGameManager.Instance.OpenChest(this);
        UIGameManager.Instance.UpdateChest(itemList, sizeChest);
    }


    public void ChestToInventory(Vector2Int _casePos)
    {
        if (itemList[_casePos.y, _casePos.x] != null)
        {
            itemList[_casePos.y, _casePos.x] = playerInventory.AddItem(itemList[_casePos.y, _casePos.x]);

            UIGameManager.Instance.UpdateChest(itemList, sizeChest);
            UIGameManager.Instance.UpdateInventory(playerInventory.GetInventory());
        }
    }


    public Vector2Int GetSize()
    {
        return sizeChest;
    }


    public Item[,] GetItems()
    {
        return itemList;
    }


    public Item AddItem(Item _newItem)
    {
        if (_newItem != null)
        {
            bool itemFind = false;

            for (int i = 0; i < sizeChest.y; i++)
            {
                for (int j = 0; j < sizeChest.x; j++)
                {
                    if (itemList[i,j] != null && itemList[i,j].itemId == _newItem.itemId)
                    {
                        if (itemList[i,j].nbItem + _newItem.nbItem < itemList[i,j].maxNbItem)
                        {
                            itemList[i,j].nbItem += _newItem.nbItem;
                            _newItem = null;
                            itemFind = true;
                        }
                        else
                        {
                            int left = (itemList[i,j].nbItem + _newItem.nbItem) - itemList[i,j].maxNbItem;
                            itemList[i,j].nbItem = itemList[i,j].maxNbItem;
                            _newItem.nbItem = left;
                        }
                    }
                }

            }
            if (itemFind == false)
            {
                for (int i = 0; i < sizeChest.y; i++)
                {
                    if (itemFind == false)
                    {
                        for (int j = 0; j < sizeChest.x; j++)
                        {

                            if (itemFind != false)
                            {
                                continue;
                            }
                            if (itemList[i,j] == null)
                            {
                                if (_newItem.nbItem <= _newItem.maxNbItem)
                                {
                                    itemList[i,j] = new Item(_newItem);
                                    _newItem = null;
                                    itemFind = true;
                                }
                                else
                                {
                                    Item tempItem = new Item(_newItem);
                                    tempItem.nbItem = tempItem.maxNbItem;
                                    _newItem.nbItem -= tempItem.maxNbItem;
                                    itemList[i,j] = new Item(tempItem);
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
}
