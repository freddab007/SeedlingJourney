using System;
using UnityEngine;

public class ChestCase : MonoBehaviour
{
    [SerializeField]Vector2Int _posCase;
    Action<Vector2Int> takeFunc;
    // Start is called before the first frame update
    void Start()
    {
        takeFunc += UIGameManager.Instance.GetOpenChest().ChestToInventory;
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
