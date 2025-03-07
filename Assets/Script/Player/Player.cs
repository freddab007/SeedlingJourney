using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

enum PlayerAction
{
    NOTHING,
    HARVEST,
    WATERING,
    PLANTSEED,
}

public class Player : MonoBehaviour
{
    PlayerInput plI;
    Rigidbody2D rb;
    //ToolBar toolBar;
    Inventory inventory;
    int columnSelect;
    Vector3 trueMousePosition = new Vector3();
    Vector2Int mouseMapPosition = new Vector2Int();
    Vector2Int playerPosTick = new Vector2Int();
    Vector2Int globalMapPosition = new Vector2Int(0, 0);

    [SerializeField][Range(1.0f, 20.0f)] float speed = 2;
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject SelectedTest;
    [SerializeField] MapManager mapManager;

    Vector2Int movementMap;
    Vector2 pos;


    GameObject objectOnMap;

    PlayerAction action;

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
        trueMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector3 mousePosition = trueMousePosition;
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


    void SetPlayerPositionTile()
    {

        playerPosTick.x = (int)playerPos.transform.position.x;
        playerPosTick.y = (int)playerPos.transform.position.y;
        if (playerPos.transform.position.y < 0)
        {
            playerPosTick.y = Mathf.FloorToInt(playerPos.transform.position.y);
        }
        if (playerPos.transform.position.x < 0)
        {
            playerPosTick.x = Mathf.FloorToInt(playerPos.transform.position.x);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.PauseGame();
        }
        if (IsGamePlaying())
        {
            if (!inventoryOpen && !chestOpen)
            {
                mouseActive = plI.currentActionMap.actions[1].ReadValue<float>();
                mouseInteract = plI.currentActionMap.actions[2].ReadValue<float>();


                SetPlayerPositionTile();
                SetMousePositionTile();
                rb.velocity = plI.currentActionMap.actions[0].ReadValue<Vector2>() * speed;

                Scroll();
                GetWhatInFront();


                if (Mathf.Abs(mouseMapPosition.x - playerPosTick.x) <= 1 && Mathf.Abs(mouseMapPosition.y - playerPosTick.y) <= 1)
                {
                    SelectedTest.SetActive(true);
                    SelectedTest.transform.position = new Vector3(mouseMapPosition.x + 0.5f, mouseMapPosition.y + 0.5f, -1);
                    if (mouseActive > 0)
                    {
                        //mapManager.ChangeTile(mouseMapPosition, GetItemEquiped(), objectOnMap);
                        MapTileManager.instance.TileChanger(mouseMapPosition, GetItemEquiped());
                    }
                    else if (mouseInteract > 0)
                    {
                        if (objectOnMap)
                        {
                            if (objectOnMap.GetComponent<Chest>())
                            {
                                inventory.OpenInventory();
                                objectOnMap.GetComponent<Chest>().OpenChest();
                                OpenChest();
                            }
                            else if (objectOnMap.GetComponent<NPC>())
                            {
                                GameManager.instance.PauseGame();
                            }
                        }
                    }
                }
                else
                {
                    SelectedTest.SetActive(false);
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            SelectedTest.SetActive(false);
        }
    }

    public void OpenChest()
    {
        chestOpen = !chestOpen;
    }

    void GetWhatInFront()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(trueMousePosition.x, trueMousePosition.y), Vector2.zero, 0f);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.GetComponent<NPC>() != null)
            {
                objectOnMap = hit[i].collider.gameObject;
                return;
            }
        }
        objectOnMap = null;
    }

    bool IsGamePlaying()
    {
        return !GameManager.instance.GetPause();
    }

    public void ChangePosBarTo0(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 0;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo1(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 1;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo2(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 2;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo3(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 3;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo4(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 4;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo5(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 5;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo6(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 6;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo7(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 7;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }

    public void ChangePosBarTo8(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
        {
            columnSelect = 8;
            UIGameManager.Instance.UpdateToolText(GetItemEquiped());
        }
    }


    public void OpenInventory(InputAction.CallbackContext callbackContext)
    {
        if (IsGamePlaying() && callbackContext.phase == InputActionPhase.Performed)
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
        movementMap = _addPos;
    }

    public void ChangPosition(Vector3 _newPos)
    {
        pos = _newPos;
    }

    public void LaunchTeleportation()
    {
        globalMapPosition += movementMap;
        rb.position = pos;
        CameraManager.instance.ChangeColliderMap(globalMapPosition);
    }

}
