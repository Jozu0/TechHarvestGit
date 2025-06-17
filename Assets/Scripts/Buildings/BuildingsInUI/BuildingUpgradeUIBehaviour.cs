    using System.Collections.Generic;
    using System.Linq;
    using DG.Tweening;
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
        [SerializeField] private GameObject craftSkillUpgradeListParent;
        public TileList tileList;

        private bool skillUpgradeInstantiated = false;
        private bool craftInstantiated = false;
        
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
            craftInstantiated = false;
            skillUpgradeInstantiated = false;
            DestroyNeededRessources();
            DestroyCraftAndSkills();
        }

        public void UpdateBuildingUIUpgrade()
        {
            DestroyCraftAndSkills();
            GetBuilding();
            bool canBeUpdated = false;
            buildingBefore.sprite = buildingsInMenu.buildingSprite[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState];
            buildingNameText.text = buildingsInMenu.buildingName + " " + IntToRoman(tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState+1);
            InstantiateSkillUpgrades();
            InstantiateCraft();
            float newHeight = 59*(
                (buildingsInMenu.upgradeSkillList.upgradeSkillList.Count)+
                (buildingsInMenu.craftList.craftList.Count));
            craftSkillUpgradeListParent.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, newHeight);
            if (tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState < buildingsInMenu.maxEvoState)
            {
                canBeUpdated = true;
                NeedsVerification();
                DisplayNeededRessources();
            }
            if (canBeUpdated)
            {
                if (isInteractable)
                {
                    buildingAfter.sprite = buildingsInMenu.buildingSprite[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState+1];
                    if (buildingsInMenu.buildingType == BuildingType.Manor)
                    {
                        buildingsInMenuList.currentManorEvolveState += 1;
                    }
                    buildingNameDescriptionText.text = IntToRoman(tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState+1) + " to " + IntToRoman((tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState+2));
                    SetAlpha(1,upgradePanelUI);
                }
                else
                {
                    buildingAfter.sprite = buildingsInMenu.buildingSprite[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState+1];
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
                SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.upgradeBuildingFX);
                SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.buildingChangeFX);

                UpdateGlobalRessources();
                PlayInteractionAnimation();
                EventManager.TriggerEvent(GameEventType.AddOrDeleteRessourceInUI, null, 0);
                actualObjectOnGrid.UpdateBuildingInWorld();
                UpdateBuildingUIUpgrade();
            }
           
        }
        
        
        public void DisplayNeededRessources()
        {
            DestroyNeededRessources();
            if (!ressourcesNeededInstantiated)
            {
                if ( buildingsInMenu == null)
                {
                    return;
                }
                for (int i = 0;
                     i < buildingsInMenu.listNeedsToEvolves[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState]
                         .needsToEvolve.Count;
                     i++)
                {
                    GameObject spawnedNeed = Instantiate(itemNeedPrefab, needsToUpdate.transform);
                    spawnedNeed.GetComponent<Image>().sprite = buildingsInMenu
                        .listNeedsToEvolves[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState]
                        .needsToEvolve[i].ressourceItemNeeded.itemSprite;
                    spawnedNeed.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        buildingsInMenu
                            .listNeedsToEvolves[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState]
                            .needsToEvolve[i].ressourceAmountNeeded.ToString() + "x";
                }

                ressourcesNeededInstantiated = true;
            }
        }

        private void InstantiateSkillUpgrades()
        {
            if (!skillUpgradeInstantiated)
            {
                foreach (var t in buildingsInMenu.upgradeSkillList.upgradeSkillList)
                {
                    GameObject instantiatedUpgradeSkill = Instantiate(updateSkillPanelPrefab, craftSkillUpgradeListParent.transform);
                    instantiatedUpgradeSkill.GetComponent<SkillUpgradeUI>().localPos = actualObjectOnGrid.localPos;
                    instantiatedUpgradeSkill.GetComponent<SkillUpgradeUI>().upgradeSkill = t;
                    instantiatedUpgradeSkill.GetComponent<SkillUpgradeUI>().objectOnGrid = actualObjectOnGrid;
                }

                skillUpgradeInstantiated = true;
            }
            
        }

        private void InstantiateCraft()
        {
            if (!craftInstantiated)
            {
                foreach (var t in buildingsInMenu.craftList.craftList)
                {
                    GameObject instantiatedCraft = Instantiate(craftItemPanelPrefab, craftSkillUpgradeListParent.transform);
                    instantiatedCraft.GetComponent<CraftUI>().localPos = actualObjectOnGrid.localPos;
                    instantiatedCraft.GetComponent<CraftUI>().craft = t;
                    instantiatedCraft.GetComponent<CraftUI>().objectOnGrid = actualObjectOnGrid;

                }

                craftInstantiated = true;
            }
            
        }

        private void DestroyCraftAndSkills()
        {
            foreach (Transform child in craftSkillUpgradeListParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            skillUpgradeInstantiated = false;
            craftInstantiated = false;
        }
        
        
        private void DestroyNeededRessources()
        {
            foreach (Transform child in needsToUpdate.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            ressourcesNeededInstantiated = false;
        }

        private void UpdateGlobalRessources()
        {
            int typeOfBuildInt = (int)buildingsInWorld.buildingType;

            if (typeOfBuildInt >= 0 && typeOfBuildInt < buildingsInMenuList.buildingInMenu.Count)
            {
                foreach (var t in buildingsInMenu.listNeedsToEvolves)
                {
                    if (t.evolveStateNeed == tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState)
                    {
                        foreach (var itemNeeded in t.needsToEvolve)
                        {
                            globalRessourceList.DeleteRessource(
                                itemNeeded.ressourceItemNeeded.ressourceType,
                                itemNeeded.ressourceAmountNeeded
                            );
                        }
                        return;
                    }
                }
               
            }
        }
        
        private void NeedsVerification()
        {
            List<BuildingsInMenu.NeedsToEvolve> needsToEvolve = buildingsInMenu.listNeedsToEvolves[tileList.GetTile(actualObjectOnGrid.localPos).currentEvolveState].needsToEvolve;
            int totalRessourceNeeded = needsToEvolve.Count;
            int totalRessourceOwned = 0;
            
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
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
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
        
        private string IntToRoman(int intToRomanise)
        {
            Dictionary<string, int> romanNumbersDictionary = new()
            {
                { "I", 1 }, { "IV", 4 }, { "V", 5 },
                { "IX", 9 }, { "X", 10 }
            };
            string romanResult = "";
            foreach(var item in romanNumbersDictionary.Reverse()) {
                if (intToRomanise <= 0) break;
                while (intToRomanise >= item.Value) {
                    romanResult += item.Key;
                    intToRomanise -= item.Value;
                }
            }
            return romanResult;
        }
        
        void PlayInteractionAnimation()
        {
            upgradePanelUI.transform.localScale = Vector3.zero;
            upgradePanelUI.transform.DOScale(Vector3.one, 0.3f)
                .SetEase(Ease.OutBack); // Effet de rebond smooth
        }
    }
