using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using GLOBALS;

public class MapControl : MonoBehaviour
{
    public List<AnimationClip> PropAnimationList;
    public Sprite DefaultTileImg;
    public List<Sprite> TileImgList;
    public GameObject TilePrefab;
    public GameObject PropPrefab;
    public int MapSize;
    public float TileSize = 1f / Globals.G_CELLSIZE;
    public float DecoChance;
    public float PropChance;
    public float PropDist;
    private GameObject[,] TileMap;
    private List<GameObject> TileList;
    private List<GameObject> PropList;

    // Start is called before the first frame update
    void Start()
    {
        PrepareMap();
        PrepareProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareMap()
    {
        TileMap = new GameObject[MapSize,MapSize];
        TileList = new List<GameObject>();

        float offset = (MapSize) / 2;

        for(int y = 0; y < MapSize-1; y++){
            for(int x = 0; x < MapSize-1; x++){

                Sprite tileImg = DefaultTileImg;

                if(Random.Range(0.0f, 1.0f) < DecoChance)
                {
                    tileImg = TileImgList[Random.Range(0, TileImgList.Count)];
                }

                var t = Instantiate(TilePrefab, new Vector3(x - offset, y - offset, 0.0f), Quaternion.identity);
                var rndr = t.GetComponent<SpriteRenderer>();
                rndr.sprite = tileImg;

                TileMap[x,y] = t;
                TileList.Add(t);
            }
        }
    }

    void PrepareProps()
    {
        PropList = new List<GameObject>();

        float offset = (MapSize) / 2;

        for(int y = 0; y < MapSize-1; y++){
            for(int x = 0; x < MapSize-1; x++){

                if(Random.Range(0.0f, 1.0f) < PropChance)
                {
                    Vector2 position = (Random.insideUnitCircle * PropDist);
                    AnimationClip anim = PropAnimationList[Random.Range(0, PropAnimationList.Count)];
                    var p = Instantiate(PropPrefab, new Vector3(x + position.x - offset, y + position.y - offset, 0.0f), Quaternion.identity);

                    var pAnim = p.GetComponent<Animator>();
                    pAnim.SetInteger("clip", Random.Range(0,5));
                    
                    PropList.Add(p);
                }
            }
        }
    }
}
