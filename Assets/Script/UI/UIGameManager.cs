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

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
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
