public class Tool : IItem
{
    Item linkItem;

    ToolType toolType;

    public Tool(ToolType _type)
    {
        toolType = _type;
    }

    public ToolType GetToolsType()
    {
        return toolType;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
