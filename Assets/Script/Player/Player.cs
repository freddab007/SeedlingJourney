using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput plI;
    Rigidbody2D rb;
    //ToolBar toolBar;
    Inventory inventory;
    int columnSelect;
    Vector2Int mouseMapPosition = new Vector2Int();

    [SerializeField][Range(1.0f, 20.0f)] float speed = 2;
    [SerializeField] GameObject playerPos;
    [SerializeField] MapManager mapManager;

    bool inventoryOpen = false;

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
        StartCoroutine(LaunchUpdate());
    }

    IEnumerator LaunchUpdate()
    {
        yield return null;

        UIGameManager.Instance.UpdateToolText(GetItemEquiped());
    }

    Item GetItemEquiped()
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


    // Update is called once per frame
    void Update()
    {
        if (!inventoryOpen)
        {
            Scroll();
            float mouseActive = plI.currentActionMap.actions[1].ReadValue<float>();
            Vector2Int playerPosTick = new Vector2Int((int)playerPos.transform.position.x, Mathf.Abs((int)playerPos.transform.position.y));
            rb.velocity = plI.currentActionMap.actions[0].ReadValue<Vector2>() * speed;

            if (mouseActive > 0 && playerPosTick.y >= 0 && playerPosTick.x >= 0)
            {

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                mouseMapPosition.x = (int)mousePosition.x;
                mouseMapPosition.y = Mathf.Abs((int)mousePosition.y);
                if (Mathf.Abs(mouseMapPosition.x - playerPosTick.x) <= 1 && Mathf.Abs(mouseMapPosition.y - playerPosTick.y) <= 1)
                {
                    mapManager.ChangeTile(mouseMapPosition, GetItemEquiped());
                }
            }
        }
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
            inventory.OpenInventory();
            inventoryOpen = !inventoryOpen;
        }
    }

}
