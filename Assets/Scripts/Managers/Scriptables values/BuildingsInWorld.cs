using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsInWorld", menuName = "Scriptable Objects/BuildingsInWorld")]
public class BuildingsInWorld : ScriptableObject
{
    public List<Sprite> buildingSpriteList = new List<Sprite>();
    public string coordinates;
    public BuildingType buildingType;
    public int evolveState;
}
