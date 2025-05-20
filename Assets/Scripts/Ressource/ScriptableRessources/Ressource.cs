using System.Collections.Generic;
using UnityEngine;

public enum RessourceType
{
    Wood,
    Rock,
    Coal,
    Plants,
    Meat,
    Gravel,
    Leather,
    Flowers,
    Wheat,
    Wool,
    Sand,
    Bone
}

[CreateAssetMenu(fileName = "Ressource", menuName = "Scriptable Objects/Ressource")]
public class Ressource : ScriptableObject
{
    [SerializeField] public int HP;
    [SerializeField] public List<Sprite> ressourceSprite = new List<Sprite>();
    [SerializeField] public List<RessourceItem> ressourceItemList = new List<RessourceItem>();
}
