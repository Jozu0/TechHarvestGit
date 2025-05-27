using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsInMenuList", menuName = "Scriptable Objects/BuildingsInMenuList")]
public class BuildingsInMenuList : ScriptableObject
{
    public List<BuildingsInMenu> buildingInMenu = new List<BuildingsInMenu>();
}
