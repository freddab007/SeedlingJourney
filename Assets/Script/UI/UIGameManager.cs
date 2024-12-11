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
    List<Image> listInventory = new List<Image>();

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
                tempCase.transform.localPosition -= new Vector3(caseWidth * 4, caseWidth, 0);
                tempCase.transform.localPosition += new Vector3(caseWidth * j, caseWidth  * i, 0);
                listInventory.Add(tempCase.GetComponentInChildren<Image>());
            }
        }
    }

    public void UpdateInventory(List<Item> _items)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            listInventory[i].sprite = Sprite.Create(_items[i].inventorySprite, new Rect(Vector2.zero, new Vector2(MapManager.tileSize, MapManager.tileSize)), Vector2.zero);
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
