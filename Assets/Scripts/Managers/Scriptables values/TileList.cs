using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileList", menuName = "Scriptable Objects/TileList")]

[System.Serializable]
public class TileList : ScriptableObject
{
    [System.Serializable]
    public struct allTiles
    {
        public Vector2 tilesCoords;
        public BuildingsInWorld buildingsInWorld;
        public int currentEvolveState;
    }
    
    [SerializeField]
    public List<TileData> tiles = new List<TileData>();

    public Dictionary<Vector2Int, TileData> tileDict = new Dictionary<Vector2Int, TileData>();
    
    private void OnEnable()
    {
        tileDict.Clear();
        foreach (var tile in tiles)
        {
            tileDict[tile.tileCoords] = tile;

        }
    }
    public TileData GetTile(Vector2Int coords)
    {
        if(tileDict.TryGetValue(coords, out TileData tile))
        {
            return tile;
        }
        else
        {
            return null;
        }
    }

    public void ModifyTile(Vector2Int coords, System.Action<TileData> modifyAction)
    {
        if (tileDict.TryGetValue(coords, out TileData tile))
        {
            modifyAction(tile);
        }
    }
    
    public void AddNewTile(TileData tile)
    {
        tiles.Add(tile);
        tileDict[tile.tileCoords] = tile;
    }
}




