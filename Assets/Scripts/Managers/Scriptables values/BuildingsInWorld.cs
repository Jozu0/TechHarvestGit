using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsInWorld", menuName = "Scriptable Objects/BuildingsInWorld")]
public class BuildingsInWorld : ScriptableObject
{

    
    [System.Serializable]
    public struct EvolveSkillSpriteSet
    {
        public List<Sprite> spritesPerSkill; // Sprites associés aux compétences
    }
    public string coordinates;
    public BuildingType buildingType;
    public int evolveState;
    
    public List<EvolveSkillSpriteSet> buildingSpriteList = new List<EvolveSkillSpriteSet>();

}
