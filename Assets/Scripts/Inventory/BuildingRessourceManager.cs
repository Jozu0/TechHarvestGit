using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BuildingRessourceManager : EventListenerBaseAwakeDestroy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotGrid;
    [SerializeField] private GlobalRessourceList ressourcesList;
    [SerializeField] private List<GameObject> ressourceLocalList;
    
    protected override (GameEventType, Action<object,float>)[] GetEventBindings()
    {
        return new (GameEventType, Action<object,float>)[]
        {   
            (GameEventType.AddOrDeleteRessourceInUI, RefreshUI)

        };
    }
    void Start()
    {
        ressourcesList.ressources = ressourcesList.ressources
            .OrderBy(r => r.ressourceType) // trie par enum dans l'ordre de définition
            .ToList();
        
        for (int i = 0; i < ressourcesList.ressources.Count; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab,slotGrid.transform);
            ressourceLocalList.Add(newSlot);
            newSlot.SetActive(true);
        }

        RefreshUI(null,0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void RefreshUI(object data,float zero)
    {
        ressourcesList.ressources = ressourcesList.ressources
            .OrderBy(r => r.ressourceType)
            .ToList();
        for (int i = 0; i < ressourceLocalList.Count; i++)
        {
            ressourceLocalList[i].GetComponent<SlotUI>().RefreshInventory(ressourcesList.ressources[i].ressourceItem.itemSprite, ressourcesList.ressources[i].ressourceAmount);
        }
    }

}
