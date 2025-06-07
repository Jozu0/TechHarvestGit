using System.Collections.Generic;
using UnityEngine;

public enum BuildingType {Manor, House, Forge, Farm, Quarry, WizardTower, Lumber}

[CreateAssetMenu(fileName = "BuildingsInMenu", menuName = "Scriptable Objects/BuildingsInMenu")]



public class BuildingsInMenu : ScriptableObject
{
    [System.Serializable]
    public struct ItemNeed
    {
        public RessourceItem ressourceItemNeeded;
        public int ressourceAmountNeeded;
    }
    [System.Serializable]
    public struct ListNeedsToEvolves
    {
        public List<NeedsToEvolve> needsToEvolve;
        public int evolveStateNeed;
    }
    
    [System.Serializable]
    public struct NeedsToEvolve
    {
        public RessourceItem ressourceItemNeeded;
        public int ressourceAmountNeeded;
    }
    
    public int maxEvoState;
    public int currentAmountOfBuildings;
    public BuildingType buildingType;
    public List<Sprite> buildingSprite = new List<Sprite>();
    public int maximumNumberOfBuildings;
    public string buildingName;
    public BuildingsInWorld buildingsInWorld;

    [SerializeField]
    public List<ItemNeed> itemNeeds = new List<ItemNeed>();
    public List<NeedsToEvolve> needsToEvolve = new List<NeedsToEvolve>();
    [SerializeField]
    public List<ListNeedsToEvolves> listNeedsToEvolves = new List<ListNeedsToEvolves>();
    public CraftList craftList;
    public UpgradeSkillList upgradeSkillList;
   
}
