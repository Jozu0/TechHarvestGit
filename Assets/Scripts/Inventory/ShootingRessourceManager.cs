using System.Collections.Generic;
using UnityEngine;

public class ShootingRessourceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private int numberOfSlot;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotGrid;
    
    [SerializeField] private List<GameObject> ressourcesList;
    public GlobalRessourceList globalRessourceList;
    public StatisticData statisticData;
    void Start()
    {
        for (int i = 0; i < numberOfSlot; i++)
        {
            ressourcesList.Add(Instantiate(slotPrefab, slotGrid.transform)); // Instantiation de tous les slotPrefab;
        }
    }
    
    public void AddItem(Ressource ressourceToAdd)
    {
        foreach (RessourceItem ressourceItem in ressourceToAdd.ressourceItemList) //foreach imbriqué mais genre max 100 itérations
        {
            foreach (GameObject ressource in ressourcesList) // On parcours la liste de chaque ressource contenu dans chaque RessourceList
            { // Verifie si la ressource existe déjà dans le slot actuel, et si le slot est vide;
                if (ressource.GetComponent<SlotUI>().actualRessourceItem!=null && 
                        // si le slot est vide
                    ressource.GetComponent<SlotUI>().actualRessourceItem.ressourceType == ressourceItem.ressourceType) 
                        // Si le slot possède le mm ressource type que l'object actuel;
                {
                    ressource.GetComponent<SlotUI>().AddMoreItem();
                        // On ajoute +n item dans la liste
                    globalRessourceList.AddRessourceGlobal(ressourceItem.ressourceType,ressourceItem, statisticData.fortuneBoost); 
                        // Rajoute le nouvel item dans l'inv global;
                    break;
                }
                else if(ressource.GetComponent<SlotUI>().actualRessourceItem==null) // Le slot est vide et puisque elseif, l'objet n'existe pas plus haut
                {
                    ressource.GetComponent<SlotUI>().AddNewItem(ressourceItem); // On ajoute le nouvel item
                    globalRessourceList.AddRessourceGlobal(ressourceItem.ressourceType,ressourceItem, statisticData.fortuneBoost); // Rajoute le nouvel item dans l'inv global;
                    break;
                }
                    
            }
        }
    }

}
