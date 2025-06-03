using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradeUIBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public BuildingsInWorld buildingsInWorld;
    [SerializeField] private BuildingsInMenu buildingsInMenu;
    [SerializeField] private BuildingsInMenuList buildingsInMenuList;
    [SerializeField] private GlobalRessourceList globalRessourceList;
    [SerializeField] public ObjectOnGrid actualObjectOnGrid;
    
    [SerializeField] private Image buildingBefore;
    [SerializeField] public Image buildingAfter;
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingNameDescriptionText;
    [SerializeField] private GameObject needsToUpdate;
    [SerializeField] private Sprite noUpdateSprite;

    [SerializeField] private GameObject itemNeedPrefab;
    [SerializeField] private bool ressourcesNeededInstantiated;
    [SerializeField] private float alphaDisable;
    [SerializeField] public bool isInteractable;
    [SerializeField] private GameObject upgradePanelUI;
    [SerializeField] private GameObject updateSkillPanelPrefab;
    [SerializeField] private GameObject craftItemPanelPrefab;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetBuilding()
    {
        if (buildingsInWorld != null)
        {
            int typeOfBuildInt = (int)buildingsInWorld.buildingType;
            buildingsInMenu = buildingsInMenuList.buildingInMenu[typeOfBuildInt];
        }
        
    }

    public void NoMoreBuildingsSelectedToUpgrade()
    {
        actualObjectOnGrid = null;
        buildingsInMenu = null;
        buildingsInWorld = null;
        DestroyNeededRessources();
    }

    public void UpdateBuildingUIUpgrade()
    {
        GetBuilding();
        bool canBeUpdated = false;
        buildingBefore.sprite = buildingsInMenu.buildingSprite[actualObjectOnGrid.actualEvolutionState];
        buildingNameText.text = buildingsInMenu.buildingName + " " + IntToRoman(actualObjectOnGrid.actualEvolutionState+1);
        if (actualObjectOnGrid.actualEvolutionState < buildingsInMenu.maxEvoState)
        {
            canBeUpdated = true;
            NeedsVerification();
            DisplayNeededRessources();
        }
        if (canBeUpdated)
        {
            if (isInteractable)
            {
                buildingAfter.sprite = buildingsInMenu.buildingSprite[actualObjectOnGrid.actualEvolutionState+1];
                buildingNameDescriptionText.text = IntToRoman(actualObjectOnGrid.actualEvolutionState+1) + " to " + IntToRoman((actualObjectOnGrid.actualEvolutionState+2));
                SetAlpha(1,upgradePanelUI);
            }
            else
            {
                buildingAfter.sprite = buildingsInMenu.buildingSprite[actualObjectOnGrid.actualEvolutionState+1];
                buildingNameDescriptionText.text = "Not Enough resources";
                SetAlpha(alphaDisable,upgradePanelUI);
            }
        }
        else
        {
            DestroyNeededRessources();
            isInteractable = false;
            SetAlpha(alphaDisable,upgradePanelUI);
            buildingAfter.sprite = noUpdateSprite;
            buildingNameDescriptionText.text = "No Upgrade Available";
        }

    }


    public void UpgradeBuilding()
    {
        if (isInteractable)
        {
            actualObjectOnGrid.UpdateBuildingInWorld();
            UpdateBuildingUIUpgrade();
        }
       
    }
    
    
    public void DisplayNeededRessources()
    {
        DestroyNeededRessources();
        if (!ressourcesNeededInstantiated)
        {
            for (int i = 0;
                 i < buildingsInMenu.listNeedsToEvolves[actualObjectOnGrid.actualEvolutionState]
                     .needsToEvolve.Count;
                 i++)
            {
                GameObject spawnedNeed = Instantiate(itemNeedPrefab, needsToUpdate.transform);
                spawnedNeed.GetComponent<Image>().sprite = buildingsInMenu
                    .listNeedsToEvolves[actualObjectOnGrid.actualEvolutionState]
                    .needsToEvolve[i].ressourceItemNeeded.itemSprite;
                spawnedNeed.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    buildingsInMenu
                        .listNeedsToEvolves[actualObjectOnGrid.actualEvolutionState]
                        .needsToEvolve[i].ressourceAmountNeeded.ToString() + "x";
            }

            ressourcesNeededInstantiated = true;
        }
    }

    private void DestroyNeededRessources()
    {
        foreach (Transform child in needsToUpdate.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        ressourcesNeededInstantiated = false;
    }
    
    private void NeedsVerification()
    {
        int totalRessourceNeeded = buildingsInMenu.itemNeeds.Count;
        int totalRessourceOwned = 0;
        List<BuildingsInMenu.NeedsToEvolve> needsToEvolve = buildingsInMenu.listNeedsToEvolves[actualObjectOnGrid.actualEvolutionState].needsToEvolve;
        
            foreach (var needed in needsToEvolve)
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
            }
            else
            {
                isInteractable = false;
            }
    }
    
    private void SetAlpha(float alpha,GameObject gameObject)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha); ;
        Image[] images = gameObject.GetComponentsInChildren<Image>(true); 
        foreach (Image img in images)
        {
            if (img.gameObject.name != "ButtonToBuild")
            {
                Color c = img.color;
                c.a = alpha;
                img.color = c;
            }
           
        }

        TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (TextMeshProUGUI tmp in texts)
        {
            Color c = tmp.color;
            c.a = alpha;
            tmp.color = c;
            tmp.ForceMeshUpdate(); }
    }
    
    private string IntToRoman(int intToRomanize)
    {
        Dictionary<string, int> romanNumbersDictionary = new()
        {
            { "I", 1 }, { "IV", 4 }, { "V", 5 },
            { "IX", 9 }, { "X", 10 }
        };
        string romanResult = "";
        foreach(var item in romanNumbersDictionary.Reverse()) {
            if (intToRomanize <= 0) break;
            while (intToRomanize >= item.Value) {
                romanResult += item.Key;
                intToRomanize -= item.Value;
            }
        }
        return romanResult;
    }
}
