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

    public struct NeedsToEvolve
    {
        public RessourceItem ressourceItemNeeded;
        public int ressourceAmountNeeded;
    }

    public int evolveState;
    public int currentAmountOfBuildings;
    public BuildingType buildingType;
    public List<Sprite> buildingSprite = new List<Sprite>();
    public int maximumNumberOfBuildings;
    public string buildingName;
    public BuildingsInWorld buildingsInWorld;

    [SerializeField]
    public List<ItemNeed> itemNeeds = new List<ItemNeed>();

    
    
   
}
