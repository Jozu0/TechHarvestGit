using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftList", menuName = "Scriptable Objects/CraftList")]
public class CraftList : ScriptableObject
{
    public List<Craft> craftList = new List<Craft>();
}
