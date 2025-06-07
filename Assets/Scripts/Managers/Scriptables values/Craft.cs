using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "Scriptable Objects/Craft")]
public class Craft : ScriptableObject
{
    [System.Serializable]
    public struct Needs
    {
        public RessourceItem ressourceItemNeeded;
        public int ressourceAmountNeeded;
    }
    
    [System.Serializable]
    public struct CraftItem
    {
        public List<Needs> needsToCraft;
        public Sprite craftSprite;
        public string craftName;
        public RessourceItem itemCrafted;
        public int amountToCraft;
    }

    public CraftItem craftItem;
}
