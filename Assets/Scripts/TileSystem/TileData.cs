using UnityEngine;

[System.Serializable]
public class TileData
{
    public Vector2Int tileCoords;
    public BuildingsInWorld buildingsInWorld;
    public int currentEvolveState;
    public int numberOfHouseMaxUpgradeSkill;
    public int numberOfForgeMaxUpgradeSkill;
    public int numberOfIronBullet;
    public int numberOfVillagerInHouse;
    public int numberOfForgingUpgradeSkill;
    public bool knifeSkill;
    public bool hoeSkill;
    public bool shearsSkill;
    public bool hammerSkill;
    public bool fishingRodSkill;
    
    public virtual void OnUpdateTick()
    {
        // Peut être vide ou hérité
    }
}
