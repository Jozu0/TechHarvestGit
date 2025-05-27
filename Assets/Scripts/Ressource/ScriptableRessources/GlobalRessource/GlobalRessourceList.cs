using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalRessourceList", menuName = "Scriptable Objects/GlobalRessourceList")]


public class GlobalRessourceList : ScriptableObject
{
    
    [System.Serializable]
    public struct GlobalRessource
    {
        public RessourceItem ressourceItem;
        public RessourceType ressourceType;
        public int ressourceAmount;
    }
    [SerializeField]
    public List<GlobalRessource> ressources = new List<GlobalRessource>();


    public void DeleteRessource(RessourceType ressourceType, int ressourceAmount)
    {
        for (int i = 0; i < ressources.Count; i++)
        {
            if (ressources[i].ressourceType == ressourceType)
            {
                if (ressources[i].ressourceAmount-ressourceAmount <= 0)
                {
                    var res = ressources[i];
                    res.ressourceAmount = 0;
                    ressources[i] = res;
                }
                else
                {
                    var res = ressources[i];
                    res.ressourceAmount-=ressourceAmount;
                    ressources[i] = res;
                }
            }
        }
    }
    
    
    // Utilisation de boucles imbriqués car perf négligeables car 12 items.
    public void AddRessource(RessourceType type,RessourceItem item, int amount)
    {
        for (int i = 0; i < ressources.Count; i++)
        {
            if (ressources[i].ressourceType == type)
            {
                AddMoreRessource(type, item, i, amount);
                return;
            }
        }
        AddNewRessource(type, item, amount);
    }

    public void AddMoreRessource(RessourceType type, RessourceItem item, int iteration, int amount)
    {
        var res = ressources[iteration];
        if (res.ressourceAmount + amount > 99)
        {
            res.ressourceAmount=99;
        }
        else
        {
            res.ressourceAmount+=amount;
        }
        ressources[iteration] = res;
    }

    public void AddNewRessource(RessourceType type, RessourceItem item, int amount)
    {
        if (ToolManager.Instance.HasTool(item.toolType))
        {
            GlobalRessource res = new GlobalRessource
            {
                ressourceItem = item,
                ressourceType = type,
                ressourceAmount = amount
            };

            ressources.Add(res);
        }
    }

}
