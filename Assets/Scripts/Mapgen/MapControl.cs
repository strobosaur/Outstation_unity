using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapControl : MonoBehaviour
{
    public List<GameObject> TileList;
    public Sprite DefaultTileImg;
    public List<Sprite> TileImgList;
    public GameObject TilePrefab;
    public int MapSize;
    public float TileSize = 1f / 16f;
    public float DecoChance;
    private GameObject[,] TileMap;
    


    // Start is called before the first frame update
    void Start()
    {
        PrepareMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareMap()
    {
        TileMap = new GameObject[MapSize,MapSize];

        float offset = (MapSize) / 2;

        for(int y = 0; y < MapSize-1; y++){
            for(int x = 0; x < MapSize-1; x++){

                Sprite tileImg = DefaultTileImg;

                if(Random.Range(0.0f, 1.0f) < DecoChance)
                {
                    tileImg = TileImgList[(int)Random.Range(0, (TileImgList.Count-1))];
                }

                var t = Instantiate(TilePrefab, new Vector3((x) - offset, (y) - offset, 0.0f), Quaternion.identity);
                var rndr = t.GetComponent<SpriteRenderer>();
                rndr.sprite = tileImg;

                //t.transform.position = new Vector3((x * TileSize) - offset, (y * TileSize) - offset, 0.0f);

                TileMap[x,y] = t;
                TileList.Add(t);
            }
        }
    }
}
