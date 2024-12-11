using UnityEngine;

public class ToolBar : MonoBehaviour
{
    Tool[] tools = new Tool[9];

    int positionBar = 0;

    // Start is called before the first frame update
    void Start()
    {
        tools[0] = new Tool(ToolType.HOE);
        tools[2] = new Tool(ToolType.WATERINGCAN);
        for (int i = 3; i < tools.Length; i++)
        {
            tools[i] = new Tool(ToolType.NONE);
        }

    }

    public void ChangePositionBar(int _pos)
    {
        positionBar = _pos;
    }

    public Tool GetToolEquiped()
    {
        return tools[positionBar];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
