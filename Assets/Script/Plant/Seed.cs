using UnityEngine;
using UnityEngine.Tilemaps;

public class Seed : MonoBehaviour, IItem
{
    SpriteRenderer spriteRenderer;
    Item linkItem;

    Vector2Int tilePos;

    bool isDead = false;

    int state = 0;

    int deathTimer = 3;

    Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlantManager.Instance.AddPlant(GrowSeed);
        PlantManager.Instance.AddSeed(this);

        spriteRenderer.sprite = Sprite.Create(linkItem.texItem[state], new Rect(0, 0, 32, 32), new Vector2(0, 1), MapManager.tileSize);
    }

    public void Init(Vector2Int _pos, Item _item, Tilemap _tileMap)
    {
        tilePos = _pos;
        linkItem = new Item(_item);
        tileMap = _tileMap;

        tileMap.SetTile(new Vector3Int(tilePos.x, tilePos.y), _item.tileItem[0]);

    }

    public Vector2Int GetPos()
    {
        return tilePos;
    }


    public void GrowSeed()
    {
        if (MapManager.instance.GetTile(tilePos) == MapManager.TypeTile.GROUNDWET && !isDead)
        {
            ++linkItem.growDay;
            deathTimer = 3;
            if (state < linkItem.texItem.Count - 3)
            {
                ++state;
                spriteRenderer.sprite = Sprite.Create(linkItem.texItem[state], new Rect(0, 0, 32, 32), new Vector2(0, 1), MapManager.tileSize);
            }
            if (linkItem.growDay >= linkItem.timeGrowth)
            {
                spriteRenderer.sprite = Sprite.Create(linkItem.texItem[linkItem.texItem.Count - 2], new Rect(0, 0, 32, 32), new Vector2(0, 1), MapManager.tileSize);
                PlantManager.Instance.SubPlant(GrowSeed);
            }
        }
        else
        {
            --deathTimer;
            if (deathTimer <= 0)
            {
                isDead = true;
                PlantManager.Instance.AddDeath(Death);
            }
        }

    }

    public void Death()
    {
        PlantManager.Instance.SubPlant(GrowSeed);
        spriteRenderer.sprite = Sprite.Create(linkItem.texItem[linkItem.texItem.Count - 1], new Rect(0, 0, 32, 32), new Vector2(0, 1), MapManager.tileSize);
    }


    public Item GetPlant()
    {
        if (isDead || linkItem.growDay < linkItem.timeGrowth)
        {
            if (isDead)
            {
                Destroy(gameObject);
            }
            return null;
        }

        return linkItem;
    }
}
