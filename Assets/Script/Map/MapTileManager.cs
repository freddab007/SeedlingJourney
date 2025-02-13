using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapTileManager : MonoBehaviour
{
    public static MapTileManager instance;


    public enum TileMapType
    {
        GROUND,
        MAPCOL,
        MAPEXTERIOR,
        PLANT,
        PLANTCOL,
        NBTILEMAP
    }


    public enum DRYWETTYPE
    {
        DRY,
        WET
    }

    [SerializeField] GameObject baseTile;

    [SerializeField] Tilemap[] TileMaps = new Tilemap[(int)TileMapType.NBTILEMAP];

    [SerializeField] NavMeshSurface navMesh;

    [SerializeField] TileData[] tileData;

    Dictionary<TileBase, TileData> dataFromTiles;

    TileBase[] dryWetTile;



    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileData)
        {
            foreach (var tile in tileData.tileBase)
            {       

                dataFromTiles.Add(tile, tileData);
            }
        }

    }

    public void ChangePlantTile(Tile _tile, Vector2Int _pos, bool _colision)
    {
        Vector3Int posTile = new Vector3Int(_pos.x, _pos.y, 0);
        if (_colision)
        {
            if (TileMaps[(int)TileMapType.GROUND].GetTile(posTile) != null)
            {
                TileMaps[(int)TileMapType.PLANTCOL].SetTile(posTile, _tile);
            }
        }
        else
        {
            if (TileMaps[(int)TileMapType.GROUND].GetTile(posTile) != null)
            {
                TileMaps[(int)TileMapType.PLANT].SetTile(posTile, _tile);
            }
        }
    }

    void AddPlant(Item _item, Vector2Int _pos)
    {
        GameObject newSprite = Instantiate(baseTile);

        newSprite.AddComponent<Seed>();
        newSprite.GetComponent<Seed>().Init(_pos, _item);
    }

    public TileBase GetTileOnMap(Vector3Int _pos, TileMapType _type)
    {
        return TileMaps[(int)_type].GetTile(_pos);
    }

    void ChangeGroundTile(Vector3Int _pos)
    {
        if (GetTileOnMap( _pos, TileMapType.GROUND) != null && GetTileOnMap(_pos, TileMapType.MAPCOL) == null && GetTileOnMap(_pos, TileMapType.MAPEXTERIOR) == null)
        {
            if (dataFromTiles[GetTileOnMap(_pos, TileMapType.GROUND)].havertable)
            {
                Debug.Log("TEST !!");
                //TileMaps[(int)TileMapType.GROUND].SetTile( _pos, dryWetTile[(int)DRYWETTYPE.DRY]);
            }
        }
    }


    public AsyncOperation StartNavMeshBake()
    {
        return navMesh.BuildNavMeshAsync();
    }

    public void TileChanger(Vector2Int _pos, Item _item)
    {
        if (_item != null)
        {
            Vector3Int pos = new Vector3Int(_pos.x, _pos.y, 0);

            if (_item.itemType == TypeItem.TOOL && _item.toolType == ToolType.HOE)
            {
                ChangeGroundTile(pos);
            }
        }
    }
}
