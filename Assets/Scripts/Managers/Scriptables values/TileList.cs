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
    public List<allTiles> tiles = new List<allTiles>();

    public void AddNewTile(Vector2 tileCoords, BuildingsInWorld buildingsInWorld, int currentEvolveState)
    {
        allTiles tile = new allTiles();
        {
            tile.tilesCoords = tileCoords;
            tile.buildingsInWorld = buildingsInWorld;
            tile.currentEvolveState = currentEvolveState;
        }
        tiles.Add(tile);
    }

    public void ModifyTile(Vector2 tileCoords, BuildingsInWorld buildingsInWorld, int currentEvolveState)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].tilesCoords == tileCoords)
            {
                var temp = tiles[i]; // copie
                temp.buildingsInWorld = buildingsInWorld;
                temp.currentEvolveState = currentEvolveState;
                tiles[i] = temp; 
                Debug.Log("Modifying tile");
            }
        }
    }

    public int GetCurrentEvolveState(Vector2 tileCoords)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].tilesCoords == tileCoords)
            {
                return tiles[i].currentEvolveState;
            }
        }

        return 0;
    }
}




