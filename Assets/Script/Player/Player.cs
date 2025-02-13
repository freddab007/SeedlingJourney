using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    PlayerInput plI;
    Rigidbody2D rb;
    //ToolBar toolBar;
    Inventory inventory;
    int columnSelect;
    Vector2Int mouseMapPosition = new Vector2Int();
    Vector2Int playerPosTick = new Vector2Int();
    Vector2Int globalMapPosition = new Vector2Int( 0, 0);

    [SerializeField][Range(1.0f, 20.0f)] float speed = 2;
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject SelectedTest;
    [SerializeField] MapManager mapManager;

    GameObject objectOnMap;

    bool inventoryOpen = false;
    bool chestOpen = false;

    float mouseActive;
    float mouseInteract;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        plI = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        columnSelect = 0;
        CameraManager.instance.ChangeColliderMap(globalMapPosition);
        StartCoroutine(LaunchUpdate());
    }

    IEnumerator LaunchUpdate()
    {
        yield return null;

        UIGameManager.Instance.UpdateToolText(GetItemEquiped());
    }

    public Item GetItemEquiped()
    {
        return inventory.GetItem(0, columnSelect);
    }


    void Scroll()
    {
        if (plI.currentActionMap.actions[3].ReadValue<float>() > 0)
        {
            columnSelect++;
            if (columnSelect > 8)
            {
                columnSelect = 8;
            }
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
        else if (plI.currentActionMap.actions[3].ReadValue<float>() < 0)
        {
            columnSelect--;
            if (columnSelect < 0)
            {
                columnSelect = 0;
            }
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }


    void SetMousePositionTile()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mouseMapPosition.x = (int)mousePosition.x;
        mouseMapPosition.y = (int)mousePosition.y;
        if (mousePosition.y < 0)
        {
            mouseMapPosition.y = Mathf.FloorToInt(mousePosition.y);
        }
        if (mousePosition.x < 0)
        {
            mouseMapPosition.x = Mathf.FloorToInt(mousePosition.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryOpen && !chestOpen)
        {
            mouseActive = plI.currentActionMap.actions[1].ReadValue<float>();
            mouseInteract = plI.currentActionMap.actions[2].ReadValue<float>();

            playerPosTick.x = (int)playerPos.transform.position.x;
            playerPosTick.y = (int)playerPos.transform.position.y;

            SetMousePositionTile();
            rb.velocity = plI.currentActionMap.actions[0].ReadValue<Vector2>() * speed;

            Scroll();
            GetWhatInFront();

            SelectedTest.transform.position = new Vector3(mouseMapPosition.x + 0.5f, mouseMapPosition.y + 0.5f, -1);

            if (mouseActive > 0)
            {

                if (Mathf.Abs(mouseMapPosition.x - playerPosTick.x) <= 1 && Mathf.Abs(mouseMapPosition.y - playerPosTick.y) <= 1)
                {
                    Debug.Log(mouseMapPosition);
                    //mapManager.ChangeTile(mouseMapPosition, GetItemEquiped(), objectOnMap);
                    MapTileManager.instance.TileChanger( mouseMapPosition, GetItemEquiped());
                }
            }
            else if (mouseInteract > 0 && playerPosTick.y >= 0 && playerPosTick.x >= 0)
            {
                if (objectOnMap)
                {
                    if (objectOnMap.GetComponent<Chest>())
                    {
                        inventory.OpenInventory();
                        objectOnMap.GetComponent<Chest>().OpenChest();
                        OpenChest();
                    }
                }
            }
        }
    }

    public void OpenChest()
    {
        chestOpen = !chestOpen;
    }

    void GetWhatInFront()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(mouseMapPosition.x + 0.1f, -mouseMapPosition.y - 0.1f), Vector2.zero, 0f);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.GetComponent<Seed>() != null)
            {
                objectOnMap = hit[i].collider.gameObject;
                return;
            }
            else if (hit[i].collider.GetComponent<Chest>() != null)
            {
                objectOnMap = hit[i].collider.gameObject;
                return;
            }
        }
        objectOnMap = null;
    }


    public void ChangePosBarTo0(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 0;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo1(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 1;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo2(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 2;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo3(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 3;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo4(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 4;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo5(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 5;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo6(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 6;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo7(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 7;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo8(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 8;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }


    public void OpenInventory(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Performed)
        {
            if (!chestOpen)
            {
                inventory.OpenInventory();
                inventoryOpen = !inventoryOpen;
            }
        }
    }

    public void AddMapPosition(Vector2Int _addPos)
    {
        globalMapPosition += _addPos;
        CameraManager.instance.ChangeColliderMap(globalMapPosition);
    }

    public void ChangPosition(Vector3 _newPos)
    {
        rb.position = _newPos;
    }

}
