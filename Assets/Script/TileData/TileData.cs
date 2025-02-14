using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tileBase;

    public bool havertable;

    public bool canBeSpray;
   
    public bool isWet;
}
