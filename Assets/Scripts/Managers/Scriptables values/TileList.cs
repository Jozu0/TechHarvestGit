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
    }
    
    [SerializeField]
    public List<allTiles> tiles = new List<allTiles>();

    public void AddNewTile(Vector2 tileCoords, BuildingsInWorld buildingsInWorld)
    {
        allTiles tile = new allTiles();
        {
            tile.tilesCoords = tileCoords;
            tile.buildingsInWorld = buildingsInWorld;
        }
        tiles.Add(tile);
    }

    public void ModifyTile(Vector2 tileCoords, BuildingsInWorld buildingsInWorld)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].tilesCoords == tileCoords)
            {
                var temp = tiles[i]; // copie
                temp.buildingsInWorld = buildingsInWorld;
                tiles[i] = temp; 
                Debug.Log("Modifying tile");
            }
        }
    }
}




