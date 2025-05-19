using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private int numberOfSlot;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotGrid;
    
    [SerializeField] private List<GameObject> ressourcesList;
    
    void Start()
    {
        for (int i = 0; i < numberOfSlot; i++)
        {
            ressourcesList.Add(Instantiate(slotPrefab, slotGrid.transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Ressource ressourceToAdd, RessourceType ressourceType)
    {

        foreach (RessourceItem ressourceItem in ressourceToAdd.ressourceItemList) //foreach imbriqué mais genre max 100 itérations
        {
            foreach (GameObject ressource in ressourcesList)
            {
                if (ressource.GetComponent<SlotUI>().actualRessourceItem!=null && ressource.GetComponent<SlotUI>().actualRessourceItem.ressourceType == ressourceType)
                {
                        ressource.GetComponent<SlotUI>().AddMoreItem();
                        break;
                }
                else if(ressource.GetComponent<SlotUI>().actualRessourceItem==null)
                {
                        ressource.GetComponent<SlotUI>().AddNewItem(ressourceItem);
                        break;
                }
                    
            }
        }
    }

}
