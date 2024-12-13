using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]Vector2Int sizeChest = new Vector2Int( 9, 3);
    Item[,] itemList = new Item[ 3, 9];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenChest()
    {
        UIGameManager.Instance.OpenChest();
        UIGameManager.Instance.UpdateChest(itemList, sizeChest);
    }
}
