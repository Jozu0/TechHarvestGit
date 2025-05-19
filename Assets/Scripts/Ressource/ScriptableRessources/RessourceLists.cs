using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RessourceList", menuName = "Scriptable Objects/RessourceLists")]
public class RessourceLists : ScriptableObject
{
    [SerializeReference] public List<Ressource> RessourceList = new List<Ressource>();
}
