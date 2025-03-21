using NavMeshPlus.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

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

    public TileBase[] dryWetTile;

    List<Vector3Int> wetGround = new List<Vector3Int>();
    List<Vector3Int> dryGround = new List<Vector3Int>();

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

    public void WetToDry()
    {
        foreach (var wetPos in wetGround)
        {
            TileMaps[(int)TileMapType.GROUND].SetTile(wetPos, dryWetTile[(int)DRYWETTYPE.DRY]);
        }
        wetGround.Clear();
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

    public void AddPlant(Item _item, Vector2Int _pos)
    {
        GameObject newSprite = Instantiate(baseTile);

        newSprite.AddComponent<Seed>();
        newSprite.GetComponent<Seed>().Init(_pos, _item);
    }

    public TileBase GetTileOnMap(Vector3Int _pos, TileMapType _type)
    {
        return TileMaps[(int)_type].GetTile(_pos);
    }

    bool NothingOnGround(Vector3Int _pos, TileMapType[] _ignore = null)
    {
        if (GetTileOnMap(_pos, TileMapType.GROUND) == null)
        {
            return false;
        }
        for (int i = 1; i < (int)TileMapType.NBTILEMAP; i++)
        {
            if (_ignore != null)
            {
                if (_ignore.Where(x => x == (TileMapType)i).Count() != 0)
                {
                    continue;
                }
            }
            if (GetTileOnMap(_pos, (TileMapType)i) != null)
            {
                return false;
            }
        }

        return true;
    }

    void ChangeGroundTile(Vector3Int _pos, ToolType _toolType)
    {
        if (NothingOnGround(_pos, new TileMapType[] { TileMapType.PLANT, TileMapType.PLANTCOL }))
        {
            if (_toolType == ToolType.HOE && dataFromTiles[GetTileOnMap(_pos, TileMapType.GROUND)].havertable)
            {
                dryGround.Add(_pos);
                TileMaps[(int)TileMapType.GROUND].SetTile(_pos, dryWetTile[(int)DRYWETTYPE.DRY]);
            }
            else if (_toolType == ToolType.WATERINGCAN && dataFromTiles[GetTileOnMap(_pos, TileMapType.GROUND)].canBeSpray && !dataFromTiles[GetTileOnMap(_pos, TileMapType.GROUND)].isWet)
            {
                wetGround.Add(_pos);
                dryGround.Remove(_pos);
                TileMaps[(int)TileMapType.GROUND].SetTile(_pos, dryWetTile[(int)DRYWETTYPE.WET]);
            }
        }
    }


    public bool CanActionOnTile(Item _item, Vector2Int _pos)
    {
        Vector3Int pos = new Vector3Int(_pos.x, _pos.y, 0);
        if (GetTileOnMap(pos, TileMapType.PLANT) != null || GetTileOnMap(pos, TileMapType.PLANTCOL) != null)
        {
            Seed tempSeed = PlantManager.Instance.GetSeedByPos(_pos);
            if (tempSeed != null)
            {
                Item itemPlant = tempSeed.GetPlant();
                if (itemPlant != null && itemPlant.itemGiver != -1)
                {
                    return true;
                }
            }
        }

        if (_item.itemType == TypeItem.TOOL)
        {
            if (NothingOnGround(pos, new TileMapType[] { TileMapType.PLANT, TileMapType.PLANTCOL }))
            {
                if (_item.toolType == ToolType.HOE && dataFromTiles[GetTileOnMap(pos, TileMapType.GROUND)].havertable)
                {
                    return true;
                }

                if (_item.toolType == ToolType.WATERINGCAN && dataFromTiles[GetTileOnMap(pos, TileMapType.GROUND)].canBeSpray && !dataFromTiles[GetTileOnMap(pos, TileMapType.GROUND)].isWet)
                {
                    return true;
                }
            }
        }
        else if (_item.itemType == TypeItem.SEED)
        {
            if (NothingOnGround(pos) && dataFromTiles[GetTileOnMap(pos, TileMapType.GROUND)].canBeSpray)
            {
                return true;
            }

        }

        return false;
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
            bool plantHarvest = false;
            if (GetTileOnMap(pos, TileMapType.PLANT) != null || GetTileOnMap(pos, TileMapType.PLANTCOL) != null)
            {
                Seed tempSeed = PlantManager.Instance.GetSeedByPos(_pos);
                if (tempSeed != null)
                {
                    Item itemPlant = tempSeed.GetPlant();
                    if (itemPlant != null && itemPlant.itemGiver != -1)
                    {
                        plantHarvest = true;
                        PlantManager.Instance.SubSeed(tempSeed);
                        Item itemGive = new Item(Inventory.data.items[itemPlant.itemGiver]);
                        itemGive.nbItem = itemPlant.numberGive;
                        ChangePlantTile(null, _pos, itemPlant.collision);
                        FindAnyObjectByType<Player>()?.GetComponent<Inventory>().AddItem(itemGive);
                        Destroy(tempSeed.gameObject);
                    }

                }
            }

            if (!plantHarvest)
            {
                if (_item.itemType == TypeItem.TOOL)
                {
                    ChangeGroundTile(pos, _item.toolType);
                }
                else if (_item.itemType == TypeItem.SEED)
                {
                    if (NothingOnGround(pos) && dataFromTiles[GetTileOnMap(pos, TileMapType.GROUND)].canBeSpray)
                    {
                        AddPlant(_item, _pos);
                    }
                }
            }
        }
    }
}
