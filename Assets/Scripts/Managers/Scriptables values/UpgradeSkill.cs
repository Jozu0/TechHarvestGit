using System.Collections.Generic;
using UnityEngine;


public enum SkillType {MoreVillagers, IronBullet}

[CreateAssetMenu(fileName = "UpgradeSkill", menuName = "Scriptable Objects/UpgradeSkill")]

public class UpgradeSkill : ScriptableObject
{
    [System.Serializable]
    public struct Needs
    {
        public RessourceItem ressourceItemNeeded;
        public int ressourceAmountNeeded;
    }
    
    
    public SkillType skillType;

    
    [System.Serializable]
    
    public struct UpgradeSkillNeeds
    {
        public List<Needs> needsToEvolve;
        public int evolveStateNeed;
        public Sprite skillSprite;
        public string skillName;
        public string skillDescription;
        public int maxAmountSkill;

    }
    
    public List<UpgradeSkillNeeds> UpgradeSkillNeedsList;
}
