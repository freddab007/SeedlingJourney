using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu( fileName = "", menuName = "Seedling Journey/Datas/Tile Data")]
public class TileData : ScriptableObject
{
    public TileBase[] tileBase;

    public bool havertable;

    public bool canBeSpray;
   
    public bool isWet;
}
