using System.Collections;
using System.Collections.Generic;
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
        temp = Resources.Load<ItemDataList>("ItemDataBase/ItemDataList");
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

}
