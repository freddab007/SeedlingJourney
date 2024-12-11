using System;
using UnityEngine;

public class InventoryCase : MonoBehaviour
{
    Inventory inventory;
    [SerializeField]Vector2Int _posCase;
    Action<Vector2Int> takeFunc;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindAnyObjectByType<Player>().GetComponent<Inventory>();
        takeFunc += inventory.TakeItem;
    }

    public void Init(Vector2Int _pos)
    {
        _posCase = _pos;
    }

    public void TakeItem()
    {
        takeFunc?.Invoke(_posCase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
