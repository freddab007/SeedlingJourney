using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    static public MapManager instance;
    [SerializeField] GameObject baseSprite;
    [SerializeField] Texture2D textureMap;
    static public int tileSize = 32;
    int nbLine;
    int nbCol;
    Vector2Int mapSize = new Vector2Int(0, 0);
    List<List<GameObject>> map = new List<List<GameObject>>();
    List<List<int>> mapType = new List<List<int>>();


    List<Vector2Int> wetGround = new List<Vector2Int>();
    List<Vector2Int> DryGround = new List<Vector2Int>();

    StreamReader reader;

    Tilemap plantTileMap;

    public enum TypeTile
    {
        WALL = 0,
        GROUND = 1,
        GROUNDDRY = 2,
        GROUNDWET = 8
    }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2Int posTile = new Vector2Int();
        nbLine = textureMap.height / tileSize;
        nbCol = textureMap.width / tileSize;
        reader = new StreamReader(Application.streamingAssetsPath + @"\Maps\Map.csv");


        while (!reader.EndOfStream)
        {
            posTile.x = 0;

            string[] currentLine = reader.ReadLine().Split(',');
            map.Add(new List<GameObject>());
            mapType.Add(new List<int>());
            for (int x = 0; x < currentLine.Length; x++)
            {

                InstanciateTile(currentLine[x], posTile);
                ++posTile.x;
                if (mapSize.y == 0)
                {
                    ++mapSize.x;
                }
            }
            ++mapSize.y;
            ++posTile.y;
        }

        reader.Close();
    }
    void ChangeSprite(int _value, Vector2Int _pos)
    {
        mapType[_pos.y][_pos.x] = _value;
        map[_pos.y][_pos.x].GetComponent<SpriteRenderer>().sprite = Sprite.Create(textureMap, new Rect((_value % nbCol) * tileSize, nbLine == 1 ? 0 : ((nbLine - 1) * tileSize - (int)Math.Floor(_value / (float)nbLine) * tileSize), tileSize, tileSize), new Vector2(0, 1), tileSize);
    }

    void AddSprite(Item _item, Vector2Int _pos)
    {
        GameObject newSprite = Instantiate(baseSprite, GetComponent<Transform>());

        newSprite.GetComponent<SpriteRenderer>().sprite = Sprite.Create(_item.texItem[0], new Rect(0, 0, 32, 32), new Vector2(0, 1), tileSize);
        newSprite.GetComponent<BoxCollider2D>().isTrigger = !_item.collision;
        newSprite.AddComponent<Seed>();
        newSprite.GetComponent<Seed>().Init(_pos, _item, plantTileMap);

        newSprite.transform.position = new Vector3(_pos.x, -_pos.y, -1);
    }
    void InstanciateTile(string _value, Vector2Int _posTile)
    {
        

        int valueRead = int.Parse(_value);
        BoxCollider2D mapTileCol;

        map[_posTile.y].Add(Instantiate(baseSprite, GetComponent<Transform>()));
        mapType[_posTile.y].Add(valueRead);
        ChangeSprite(valueRead, _posTile);

        mapTileCol = map[_posTile.y][_posTile.x].GetComponent<BoxCollider2D>();
        if (valueRead == (int)TypeTile.WALL)
        {
            mapTileCol.enabled = true;
        }
        else if (valueRead == (int)TypeTile.GROUND)
        {
            mapTileCol.enabled = false;
        }

        map[_posTile.y][_posTile.x].transform.position = new Vector3(_posTile.x, -_posTile.y);
    }

    GameObject GetWhatInFront(Vector2Int _pos)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2( _pos.x + 0.1f, -_pos.y - 0.1f), Vector2.zero, 0f);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.GetComponent<Seed>() != null)
            {
                return hit[i].collider.gameObject;
            }
        }

        return null;
    }

    public void ChangeTile(Vector2Int _pos, Item _item, GameObject _objectInFront)
    {
        if (_item != null)
        {
            if (_item.itemType == TypeItem.TOOL && _item.toolType == ToolType.HOE)
            {
                if (!_objectInFront)
                {
                    if (mapType[_pos.y][_pos.x] == (int)TypeTile.GROUND)
                    {
                        ChangeSprite((int)TypeTile.GROUNDDRY, _pos);
                    }
                }
                else if (_objectInFront.GetComponent<Seed>())
                {
                    Seed tempSeed = _objectInFront.GetComponent<Seed>();
                    if (tempSeed.GetPlant() != null)
                    {
                        if (tempSeed.GetPlant().itemGiver != -1)
                        {
                            Item item = new Item(Inventory.data.items[tempSeed.GetPlant().itemGiver]);
                            item.nbItem = tempSeed.GetPlant().numberGive;
                            Destroy(_objectInFront);
                            FindAnyObjectByType<Player>()?.GetComponent<Inventory>().AddItem(item);
                        }
                    }
                }
            }
            else if (mapType[_pos.y][_pos.x] == (int)TypeTile.GROUNDDRY && _item.itemType == TypeItem.TOOL && _item.toolType == ToolType.WATERINGCAN)
            {
                AddWet(_pos);
                ChangeSprite((int)TypeTile.GROUNDWET, _pos);
            }
            else if ((mapType[_pos.y][_pos.x] == (int)TypeTile.GROUNDWET || mapType[_pos.y][_pos.x] == (int)TypeTile.GROUNDDRY) && _item.itemType == TypeItem.SEED && _objectInFront == null)
            {
                AddSprite(_item, _pos);
            }
        }
    }

    void AddWet(Vector2Int _pos)
    {
        wetGround.Add(_pos);
    }

    public void WetToDry()
    {
        for (int i = 0; i < wetGround.Count; i++)
        {
            ChangeSprite((int)TypeTile.GROUNDDRY, wetGround[i]);
        }
        wetGround.Clear();
    }

    public TypeTile GetTile(Vector2Int _pos)
    {
        return (TypeTile)mapType[_pos.y][_pos.x];
    }
}
