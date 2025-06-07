using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int width, height;
    [SerializeField] private ObjectOnGrid objectOnGrid;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject tileObjectList;
    [SerializeField] private TileList tileList;
    [SerializeField] private GameObject tileObjectPrefab;
    [SerializeField] private Color lightGreen;
    [SerializeField] private Color darkGreen;

    void Start()
    {
        if (tileList.tiles.Count < 1)
        {
            GenerateGrid();
        }
        SpawnGrid();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileData newTile = new TileData
                {
                    tileCoords = new Vector2Int(x, y),
                    buildingsInWorld = null,
                    currentEvolveState = 0,
                    numberOfHouseMaxUpgradeSkill = 0,
                    numberOfForgeMaxUpgradeSkill = 0,
                    numberOfVillagerInHouse = 0,
                    numberOfForgingUpgradeSkill = 0,
                    knifeSkill = false,
                    hoeSkill = false,
                    shearsSkill = false,
                    hammerSkill = false,
                    fishingRodSkill = false,
                };

                tileList.AddNewTile(newTile);
                
            }
        }
        // cameraTransform.position = new Vector3((float)width / 2, (float)height / 2, -10);
    }

    void SpawnGrid()
    {
        foreach (var tile in tileList.tiles)
        {
            GameObject instance = Instantiate(
                tileObjectPrefab,
                new Vector2((float)(1.5f * tile.tileCoords.x + 0.5f), (float)(1.5f * tile.tileCoords.y + 0.5f)),
                Quaternion.identity,
                tileObjectList.transform
            );
            if (((int)tile.tileCoords.x + (int)tile.tileCoords.y) % 2 == 1)
            {
                instance.GetComponent<SpriteRenderer>().color = lightGreen;
            }
            else
            {
                instance.GetComponent<SpriteRenderer>().color = darkGreen;
            }
            instance.GetComponent<ObjectOnGrid>().buildingsInWorld = tile.buildingsInWorld;
            instance.name = tile.tileCoords.x + "," + tile.tileCoords.y;
        }
        tileObjectList.transform.position = new Vector2(0.255f,0.255f);
    }
}
