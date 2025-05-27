using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class ObjectOnGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highLight;
    [SerializeField] private bool isEmpty;
    [SerializeField] private Color emptyColor;
    [SerializeField] private Color fullColor;
    [SerializeField] private GridPlacementSystem gridPlacementSystem;
    [SerializeField] public BuildingsInWorld buildingsInWorld;
    [SerializeField] private GameObject buildingOnGrid;
    [SerializeField] private TileList tileList;
    [SerializeField] private BuildingsInMenuList buildingsInMenuList;
    [SerializeField] private GlobalRessourceList globalRessourceList;
    void Start()
    {
        gridPlacementSystem = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridPlacementSystem>();
        gridPlacementSystem.isBuildingSelected = false;
        UpdateContent();
    }

    void UpdateContent()
    {
        if (buildingsInWorld==null)
        {
            isEmpty = true;
            buildingOnGrid.SetActive(false);
        }
        else
        {
            isEmpty = false;
            buildingOnGrid.SetActive(true);
            buildingOnGrid.GetComponent<SpriteRenderer>().sprite = buildingsInWorld.buildingSpriteList[buildingsInWorld.evolveState];

        }
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    void OnMouseEnter()
    {
        if (gridPlacementSystem.isBuildingSelected)
        {
            highLight.SetActive(true);
            if (isEmpty == false)
            {
                highLight.GetComponent<SpriteRenderer>().color = fullColor;

            }
            else
            {
                highLight.GetComponent<SpriteRenderer>().color = emptyColor;
                buildingOnGrid.SetActive(true);
                buildingOnGrid.GetComponent<SpriteRenderer>().sprite = gridPlacementSystem.buildingSpriteSelected;
            }
        }
        
       
    }

    void OnMouseRightClick()
    {
        gridPlacementSystem.NoMoreBuildings();
    }
    
    void OnMouseExit()
    {
        
        highLight.SetActive(false);
        if (buildingsInWorld == null)
        {
            buildingOnGrid.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    
        if (gridPlacementSystem.isBuildingSelected && isEmpty == true)
        {
            buildingsInWorld = gridPlacementSystem.buildingsSelected;
            UpdateContent();
            gridPlacementSystem.NoMoreBuildings();
            tileList.ModifyTile(
                new Vector2(
                    ((transform.position.x - 0.5f) / 1.5f) - 0.17f,
                    ((transform.position.y - 0.5f) / 1.5f) - 0.17f
                ), 
                buildingsInWorld);
            UpdateBuildingAmount();
            UpdateGlobalRessources();
            EventManager.TriggerEvent(GameEventType.AddOrDeleteRessourceInUI, null, 0);
        }
    }

    void UpdateBuildingAmount()
    {
        int typeOfBuildInt = (int)buildingsInWorld.buildingType;
        buildingsInMenuList.buildingInMenu[typeOfBuildInt].currentAmountOfBuildings++;
         // Rajoute un nombre à la liste 
    }

    void UpdateGlobalRessources()
    {
        int typeOfBuildInt = (int)buildingsInWorld.buildingType;

        if (typeOfBuildInt >= 0 && typeOfBuildInt < buildingsInMenuList.buildingInMenu.Count)
        {
            List<BuildingsInMenu.ItemNeed> itemNeed = buildingsInMenuList.buildingInMenu[typeOfBuildInt].itemNeeds;

            foreach (var itemNeeded in itemNeed)
            {
                globalRessourceList.DeleteRessource(
                    itemNeeded.ressourceItemNeeded.ressourceType,
                    itemNeeded.ressourceAmountNeeded
                );
            }
        }
    }

  
}

