using System.Collections.Generic;
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
    [SerializeField] GameObject prefabCase;
    RawImage[,] listInventory;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        listInventory = new RawImage[3,9];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject tempCase = Instantiate(prefabCase, panelInventory.transform);
                float caseWidth = tempCase.GetComponent<RectTransform>().rect.width;
                tempCase.GetComponent<RectTransform>().transform.localPosition -= new Vector3(caseWidth * 4, -caseWidth, 0);
                tempCase.GetComponent<RectTransform>().transform.localPosition += new Vector3(caseWidth * j, -caseWidth * i, 0);
                listInventory[i,j] = tempCase.GetComponentInChildren<RawImage>();
            }
        }
    }

    public void UpdateInventory(Item[][] _inventory)
    {
        if (!panelInventory.activeSelf)
        {
            panelInventory.SetActive(true);
            for (int i = 0; i < _inventory.Length; i++)
            {
                for (int j = 0; j < _inventory.Length; j++)
                {
                    if (_inventory[i][j] != null)
                    {
                        listInventory[i,j].texture = _inventory[i][j].inventorySprite;
                    }
                }
            }
        }
        else
        {
            panelInventory.SetActive(false);
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

    }
}
