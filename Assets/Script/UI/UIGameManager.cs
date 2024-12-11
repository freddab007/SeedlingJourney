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
    List<RawImage> listInventory = new List<RawImage>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject tempCase = Instantiate(prefabCase, panelInventory.transform);
                float caseWidth = tempCase.GetComponent<RectTransform>().rect.width ;
                tempCase.GetComponent<RectTransform>().transform.localPosition -= new Vector3(caseWidth * 4, -caseWidth, 0);
                tempCase.GetComponent<RectTransform>().transform.localPosition += new Vector3(caseWidth * j, -caseWidth  * i, 0);
                listInventory.Add(tempCase.GetComponentInChildren<RawImage>());
            }
        }
    }

    public void UpdateInventory(List<Item> _items)
    {
        if (!panelInventory.activeSelf)
        {
            panelInventory.SetActive(true);
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] != null)
                {
                    listInventory[i].texture = _items[i].inventorySprite;
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
