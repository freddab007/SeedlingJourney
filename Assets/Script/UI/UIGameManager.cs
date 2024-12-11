using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    [SerializeField] TextMeshProUGUI toolText;
    [SerializeField] TextMeshProUGUI yearText;
    [SerializeField] TextMeshProUGUI seasonText;
    [SerializeField] TextMeshProUGUI dayText;


    [SerializeField] GameObject panelInventory;
    [SerializeField] GameObject prefabCase;

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
                tempCase.transform.localPosition -= new Vector3(120 * 4, 120, 0);
                tempCase.transform.localPosition += new Vector3(120 * j, 120 * i, 0);
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

    }
}
