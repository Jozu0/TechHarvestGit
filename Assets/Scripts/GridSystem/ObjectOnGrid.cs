using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ObjectOnGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highLight;
    [SerializeField] private GameObject flashHightLight;
    [SerializeField] private bool isEmpty;
    [SerializeField] private Color emptyColor;
    [SerializeField] private Color fullColor;
    [SerializeField] private GridPlacementSystem gridPlacementSystem;
    [SerializeField] public BuildingsInWorld buildingsInWorld;
    [SerializeField] private GameObject buildingOnGrid;
    [SerializeField] private TileList tileList;
    [SerializeField] private BuildingsInMenuList buildingsInMenuList;
    [SerializeField] private GlobalRessourceList globalRessourceList;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private BuildingUpgradeUIBehaviour upgradeUIPanel;
    [SerializeField] private StatisticData statisticData;
    private static SpriteRenderer currentSpriteRenderer = null;
    private static Tween flashTween = null;
    public Vector2Int localPos;
    void Start()
    {
        gridPlacementSystem = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridPlacementSystem>();
        UIManager = GameObject.FindGameObjectWithTag("MainUIPanel").GetComponent<UIManager>();
        upgradeUIPanel = UIManager.UIUpgradeManagementGameObject.GetComponent<BuildingUpgradeUIBehaviour>();
        gridPlacementSystem.isBuildingSelected = false;
        localPos = new Vector2Int(
                (int)(((transform.position.x - 0.5f) / 1.5f) - 0.17f),
                (int)(((transform.position.y - 0.5f) / 1.5f) - 0.17f));
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
            GetSprite();
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

    public void OnMouseCancel()
    {
        gridPlacementSystem.NoMoreBuildings();
        upgradeUIPanel.NoMoreBuildingsSelectedToUpgrade();
        UIManager.ChangeCurrentToRessourceManagement(); 
        if (flashTween != null && flashTween.IsActive())
        {
            flashTween.Kill();
        }

        if (currentSpriteRenderer != null)
        {
            SpriteRenderer sr = currentSpriteRenderer;
            sr.DOKill();
            sr.color = new Color(1, 1, 1, 1);

            sr.DOFade(0f, 0.2f).OnComplete(() =>
            {
                sr.gameObject.SetActive(false);
                currentSpriteRenderer = null;
                flashTween = null;
            });
        }   
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
            if (buildingsInWorld.buildingType == BuildingType.House)
            {
                statisticData.totalVillagers +=1;
            }
            UpdateContent();
            PlayInteractionAnimation();
            gridPlacementSystem.NoMoreBuildings();
            tileList.ModifyTile(localPos, 
                tile => tile.buildingsInWorld = buildingsInWorld);
            UpdateBuildingAmount();
            UpdateGlobalRessources();
            EventManager.TriggerEvent(GameEventType.AddOrDeleteRessourceInUI, null, 0);
            
            return;
        }
        
        

        if (isEmpty == false)
        {
            
            upgradeUIPanel.buildingsInWorld = buildingsInWorld;
            upgradeUIPanel.actualObjectOnGrid = this;
            PlayInteractionAnimation();
            if (upgradeUIPanel.isInteractable)
            {
                upgradeUIPanel.DisplayNeededRessources();
            }
            upgradeUIPanel.UpdateBuildingUIUpgrade();
            FlashHighlight();
            UIManager.ChangeCurrentToUpgradeManagement();

        }
        
    }


    public void UpdateBuildingInWorld()
    {
        tileList.GetTile(localPos).currentEvolveState += 1;
        tileList.ModifyTile(localPos, tile => tile.currentEvolveState = tileList.GetTile(localPos).currentEvolveState);
        GetSprite();
    }

    public void UpdateSkillInWorld()
    {
        GetSprite();
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

    void FlashHighlight()
        // Si on a déjà un flash actif SUR CE même sprite, on kill l'ancien
    {
        SpriteRenderer sr = flashHightLight.GetComponent<SpriteRenderer>();
        // Si un tween est déjà actif SUR UN AUTRE sprite, on kill ce tween ET on désactive son GameObject
        if (flashTween != null && flashTween.IsActive())
        {
            flashTween.Kill();
            if (currentSpriteRenderer != null)
                currentSpriteRenderer.gameObject.SetActive(false);
        }
            // Active ce flash et reset alpha
        flashHightLight.SetActive(true);
        sr.color = new Color(1, 1, 1, 1);
            // On stocke la nouvelle référence globale
        currentSpriteRenderer = sr;
            // On lance le tween global sur CE sprite
        flashTween = sr.DOFade(0f, 0.4f)
            .SetId("UniqueFlashTween")
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    
    void PlayInteractionAnimation()
    {
        buildingOnGrid.transform.localScale = Vector3.zero;
        buildingOnGrid.transform.DOScale(Vector3.one, 0.3f)
            .SetEase(Ease.OutBack); // Effet de rebond smooth
    }


    public void GetSprite()
    {
        switch (buildingsInWorld.buildingType)
        {
            case BuildingType.House:
            {
                int currentEvolveState = tileList.GetTile(localPos).currentEvolveState;     
                int currentSkillLevel = tileList.GetTile(localPos).numberOfVillagerInHouse - 1;
                buildingOnGrid.GetComponent<SpriteRenderer>().sprite =
                    buildingsInWorld.buildingSpriteList[currentEvolveState]
                        .spritesPerSkill[GetSkillSprite(tileList.GetTile(localPos).currentEvolveState,tileList.GetTile(localPos).numberOfVillagerInHouse)];
                break;
            }
            default:
            {
                buildingOnGrid.GetComponent<SpriteRenderer>().sprite =
                    buildingsInWorld.buildingSpriteList[tileList.GetTile(localPos).currentEvolveState]
                        .spritesPerSkill[0];
                break;
            }
        }
    }
    
    int GetSkillSprite(int evolve, int skill)
    {
        int index = skill;
        if (skill > 2)
        {
            index -= (2*evolve);
        }

        // Debug log
        Debug.Log($"[GetSkillSprite] evolve: {evolve}, skill: {skill}, result index: {index}");

        return index;
    }
}

