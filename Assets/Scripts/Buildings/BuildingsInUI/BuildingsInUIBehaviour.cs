using System;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsInUIBehaviour : EventListenerBaseAwakeDestroy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] public BuildingsInMenu buildingsInMenu;
    [SerializeField] private TextMeshProUGUI buildingAmountText;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private GameObject buildingNeedListGrid;
    [SerializeField] private Image buildingSpriteComponent;
    public GlobalRessourceList globalRessourceList;
    [SerializeField] private GameObject neededItemPrefab;
    private bool isInteractable;
    [SerializeField] private GridPlacementSystem gridPlacementSystem;
    [SerializeField] private BuildingInUIInstantiate buildingInUIInstantiate;

    [SerializeField] private float alphaDisable;
    
    protected override (GameEventType, Action<object,float>)[] GetEventBindings()
    {
        return new (GameEventType, Action<object,float>)[]
        {   
            (GameEventType.AddOrDeleteRessourceInUI, SetBuildingAmountText),
            (GameEventType.AddOrDeleteRessourceInUI, NeedsVerification),
            (GameEventType.AddOrDeleteRessourceInUI, UpdateInteraction),
            (GameEventType.NoMoreBuildingSelected, UpdateBuildingSelected)
        };
    }
    
    
    
    void Start()
    {
        gridPlacementSystem = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridPlacementSystem>();
        buildingInUIInstantiate = GameObject.FindGameObjectWithTag("BuildingInUIContent").GetComponent<BuildingInUIInstantiate>();
        SetBuildingAmountText(null, 0);
        buildingNameText.text = buildingsInMenu.buildingName;
        buildingSpriteComponent.sprite = buildingsInMenu.buildingSprite[0];
        buildingsInMenu.itemNeeds = buildingsInMenu.itemNeeds
            .OrderBy(r => r.ressourceItemNeeded.ressourceType) // trie par enum dans l'ordre de définition
            .ToList();
        DisplayNeededRessources();
        NeedsVerification(null, 0);
        UpdateInteraction(null, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBuildingAmountText(object none, float zero)
    {
        buildingAmountText.text = buildingsInMenu.currentAmountOfBuildings+"/"+buildingsInMenu.maximumNumberOfBuildings;
    }

    void UpdateBuildingSelected(object none, float zero)
    {
        if (gridPlacementSystem.isBuildingSelected == true)
        {
            foreach (var buildingUIBehaviour in buildingInUIInstantiate.buildingsInUIBehaviourList)
            {
                buildingUIBehaviour.isInteractable = false;
                buildingUIBehaviour.UpdateInteraction(null, 0);
            }
           
        }
        else
        {
            isInteractable = true;
            UpdateInteraction(null, 0); 
        }
    }
    
    void UpdateInteraction(object none, float zero)
    {
        SetAlpha(isInteractable ? 1f : alphaDisable);
    }

    public void SelectRessource()
    {
        if (isInteractable)
        {
            gridPlacementSystem.isBuildingSelected = true;
            gridPlacementSystem.buildingsSelected = buildingsInMenu.buildingsInWorld;
            UpdateBuildingSelected(null, 0);
        }
    }
    
    private void DisplayNeededRessources()
    {
        for (int i = 0; i < buildingsInMenu.itemNeeds.Count; i++)
        {
            GameObject spawnedNeed = Instantiate(neededItemPrefab, buildingNeedListGrid.transform);
            spawnedNeed.GetComponent<Image>().sprite = buildingsInMenu.itemNeeds[i].ressourceItemNeeded.itemSprite;
            spawnedNeed.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buildingsInMenu.itemNeeds[i].ressourceAmountNeeded.ToString()+"x";
        }
    }
    private void NeedsVerification(object none, float zero)
    {
        if (buildingsInMenu.currentAmountOfBuildings < buildingsInMenu.maximumNumberOfBuildings)
        {
            int totalRessourceNeeded = buildingsInMenu.itemNeeds.Count;
            int totalRessourceOwned = 0;
            foreach (var needed in buildingsInMenu.itemNeeds)
            {
                foreach (var owned in globalRessourceList.ressources)
                {
                    if (needed.ressourceItemNeeded == owned.ressourceItem)
                    {
                        if (owned.ressourceAmount >= needed.ressourceAmountNeeded)
                        {
                            totalRessourceOwned++;
                        }

                        break;
                    }
                }
            }

            if (totalRessourceNeeded == totalRessourceOwned)
            {
                isInteractable = true;
                return;
            }
            else
            {
                isInteractable = false;
                return;
            }
        }
        isInteractable = false;
        return;
    }
    
    
    
    private void SetAlpha(float alpha)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha); ;
        Image[] images = GetComponentsInChildren<Image>(true); 
        foreach (Image img in images)
        {
            if (img.gameObject.name != "ButtonToBuild")
            {
                Color c = img.color;
                c.a = alpha;
                img.color = c;
            }
           
        }

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI tmp in texts)
        {
            Color c = tmp.color;
            c.a = alpha;
            tmp.color = c;
            tmp.ForceMeshUpdate(); }
    }
}
