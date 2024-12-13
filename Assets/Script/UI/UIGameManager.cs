using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    [SerializeField] TextMeshProUGUI toolText;
    [SerializeField] TextMeshProUGUI yearText;
    [SerializeField] TextMeshProUGUI seasonText;
    [SerializeField] TextMeshProUGUI dayText;


    [SerializeField] GameObject panelInventory;
    [SerializeField] GameObject panelChest;
    [SerializeField] GameObject prefabCase;
    [SerializeField] Texture2D emptyCase;
    [SerializeField] RawImage mouseItem;
    RawImage[,] listInventory;
    RawImage[,] listChest;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        listInventory = new RawImage[3, 9];
        panelInventory.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject tempCase = Instantiate(prefabCase, panelInventory.transform);
                float caseWidth = tempCase.GetComponent<RectTransform>().rect.width;
                tempCase.GetComponent<RectTransform>().transform.localPosition -= new Vector3(caseWidth * 4, -caseWidth, 0);
                tempCase.GetComponent<RectTransform>().transform.localPosition += new Vector3(caseWidth * j, -caseWidth * i, 0);
                tempCase.GetComponent<InventoryCase>().Init(new Vector2Int(j, i));
                listInventory[i, j] = tempCase.GetComponentInChildren<RawImage>();
            }
        }
        panelInventory.SetActive(false);

        listChest = new RawImage[3, 9];
        panelChest.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject tempCase = Instantiate(prefabCase, panelChest.transform);
                float caseWidth = tempCase.GetComponent<RectTransform>().rect.width;
                tempCase.GetComponent<RectTransform>().transform.localPosition -= new Vector3(caseWidth * 4, -caseWidth, 0);
                tempCase.GetComponent<RectTransform>().transform.localPosition += new Vector3(caseWidth * j, -caseWidth * i, 0);
                tempCase.GetComponent<InventoryCase>().Init(new Vector2Int(j, i));
                listChest[i, j] = tempCase.GetComponentInChildren<RawImage>();
            }
        }
        panelChest.SetActive(false);
    }

    public void OpenInventory()
    {
        panelInventory.transform.parent.gameObject.SetActive(!panelInventory.transform.parent.gameObject.activeSelf);
        panelInventory.SetActive(!panelInventory.activeSelf);
        panelInventory.GetComponent<RectTransform>().localPosition = Vector3.zero;
        panelInventory.GetComponentInParent<Image>(true).enabled = true;
    }

    public void OpenChest()
    {
        panelChest.transform.parent.gameObject.SetActive(!panelChest.transform.parent.gameObject.activeSelf);
        panelChest.SetActive(!panelChest.activeSelf);
        panelInventory.GetComponent<RectTransform>().localPosition = new Vector3(0, -140);
        panelInventory.GetComponentInParent<Image>().enabled = false;
        panelChest.GetComponent<RectTransform>().localPosition = new Vector3(0, 140);
    }

    public void UpdateInventory(Item[][] _inventory)
    {

        for (int i = 0; i < _inventory.Length; i++)
        {
            for (int j = 0; j < _inventory[i].Length; j++)
            {
                if (_inventory[i][j] != null)
                {
                    listInventory[i, j].texture = _inventory[i][j].inventorySprite;
                }
                else
                {
                    listInventory[i, j].texture = emptyCase;
                }
            }
        }
    }

    public void UpdateChest(Item[,] _inventory, Vector2Int _size)
    {

        for (int i = 0; i < _size.y; i++)
        {
            for (int j = 0; j < _size.x; j++)
            {
                if (_inventory[i, j] != null)
                {
                    listChest[i, j].texture = _inventory[i, j].inventorySprite;
                }
                else
                {
                    listChest[i, j].texture = emptyCase;
                }
            }
        }
    }

    public void UpdateMousItem(Item _item)
    {
        if (panelInventory.activeSelf)
        {
            if (_item != null)
            {
                mouseItem.texture = _item.inventorySprite;
            }
            else
            {
                mouseItem.texture = emptyCase;
            }
        }
    }

    public void UpdateToolText(Item _item)
    {
        if (_item != null)
        {
            toolText.text = _item.itemName + " nb : " + _item.nbItem.ToString();
        }
        else
        {
            toolText.text = "Empty";
        }
    }

    public void UpdateYearText(string _text)
    {
        yearText.text = "Year : " + _text;
    }

    public void UpdateSeasonText(string _text)
    {
        seasonText.text = _text;
    }

    public void UpdateDayText(string _text)
    {
        dayText.text = _text;
    }

    // Update is called once per frame
    void Update()
    {
        if (panelInventory.activeSelf)
        {
            mouseItem.gameObject.transform.position = Input.mousePosition + Vector3.right * 25 + Vector3.down * 25;
        }
    }
}
