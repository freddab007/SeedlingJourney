using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTileManager : MonoBehaviour
{
    public static MapTileManager instance;


    enum TileMapType
    {
        GROUND,
        MAPCOL,
        WALL,
        PLANT,
        PLANTCOL,
        NBTILEMAP
    }

    [SerializeField] GameObject baseTile;

    [SerializeField] Tilemap[] TileMaps = new Tilemap[(int)TileMapType.NBTILEMAP];

    [SerializeField] NavMeshSurface navMesh;


    public void ChangePlantTile(Tile _tile, Vector2Int _pos, bool _colision)
    {
        Vector3Int posTile = new Vector3Int(_pos.x, _pos.y, 0);
        if (_colision)
        {
            if (TileMaps[(int)TileMapType.GROUND].GetTile(posTile) != null)
            {
                TileMaps[(int)TileMapType.PLANTCOL].SetTile(posTile, _tile);
                //TileMaps[(int)TileMapType.PLANTCOL].GetComponent<TilemapRenderer>().enabled = false;
                //TileMaps[(int)TileMapType.PLANTCOL].GetComponent<TilemapRenderer>().enabled = true;
            }
        }
        else
        {
            if (TileMaps[(int)TileMapType.GROUND].GetTile(posTile) != null)
            {
                TileMaps[(int)TileMapType.PLANT].SetTile(posTile, _tile);
                //TileMaps[(int)TileMapType.PLANT].GetComponent<TilemapRenderer>().enabled = false;
                //TileMaps[(int)TileMapType.PLANT].GetComponent<TilemapRenderer>().enabled = true;
            }
        }
    }

    void AddPlant(Item _item, Vector2Int _pos)
    {
        GameObject newSprite = Instantiate(baseTile);

        newSprite.AddComponent<Seed>();
        newSprite.GetComponent<Seed>().Init(_pos, _item);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }


    public AsyncOperation StartNavMeshBake()
    {
        return navMesh.BuildNavMeshAsync();
    }
}
