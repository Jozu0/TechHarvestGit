using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TileList tileList;
    [SerializeField] public Vector2Int localPos;
    [SerializeField] public UpgradeSkill upgradeSkill;
    private bool canUpgradeSkill = false;
    [SerializeField] private GlobalRessourceList globalRessourceList;
    [SerializeField] public ObjectOnGrid objectOnGrid;
    [SerializeField] private Image skillSprite;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDescriptionText;
    [SerializeField] private GameObject needList;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject itemNeedPrefab;
    [SerializeField] public StatisticData statisticData;
    private bool ressourcesNeededInstantiated = false;

    void Start()
    {
        UpdateSkillUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonSkillPressed()
    {
        UpdateSkillUI();
        if (canUpgradeSkill)
        {
            UpdateGlobalRessources();
            PlayInteractionAnimation();
            switch (upgradeSkill.skillType)
            {
                case SkillType.MoreVillagers:
                {
                    tileList.GetTile(localPos).numberOfVillagerInHouse += 1;
                    switch(tileList.GetTile(localPos).currentEvolveState)
                    {
                        case 0:
                        {
                            statisticData.totalVillagers += 1;
                            break;
                        }
                        case 1:
                        {
                            statisticData.totalVillagers += 3;
                            break;
                        }
                    }
                    
                    UpdateSkillUI();
                    objectOnGrid.GetSprite();
                    break;
                }
                case SkillType.IronBullet:
                {
                    tileList.GetTile(localPos).numberOfIronBullet += 1;
                    statisticData.bulletDamage += 0.5f;
                    UpdateSkillUI();
                    break;
                }
            }
        }
        EventManager.TriggerEvent(GameEventType.AddOrDeleteRessourceInUI, null, 0);

    }


    private void UpdateGlobalRessources()
    {
        foreach (var t in upgradeSkill.UpgradeSkillNeedsList)
        {
            if (t.evolveStateNeed == tileList.GetTile(localPos).currentEvolveState)
            {
                foreach (var itemNeeded in t.needsToEvolve)
                {
                    globalRessourceList.DeleteRessource(
                        itemNeeded.ressourceItemNeeded.ressourceType,
                        itemNeeded.ressourceAmountNeeded);
                }

                return;
            }
        }
    }


    private void UpdateSkillUI()
    {
        int currentEvolveState = tileList.GetTile(localPos).currentEvolveState;
        skillSprite.sprite = upgradeSkill.UpgradeSkillNeedsList[currentEvolveState].skillSprite;
        skillNameText.text = upgradeSkill.UpgradeSkillNeedsList[currentEvolveState].skillName;
        skillDescriptionText.text = upgradeSkill.UpgradeSkillNeedsList[currentEvolveState].skillDescription;
        switch (upgradeSkill.skillType)
        {
            case SkillType.MoreVillagers:
            {
                amountText.text = tileList.GetTile(localPos).numberOfVillagerInHouse.ToString()
                                  + "/"
                                  + upgradeSkill.UpgradeSkillNeedsList[currentEvolveState].maxAmountSkill;
                break;
            }
            case SkillType.IronBullet:
            {
                amountText.text = tileList.GetTile(localPos).numberOfIronBullet.ToString()
                                  +"/"
                                  + upgradeSkill.UpgradeSkillNeedsList[currentEvolveState].maxAmountSkill;
                break;
            }
        }

        NeedsVerification();
        DisplayNeededRessources();
        if (canUpgradeSkill)
        {
            SetAlpha(1);
        }
        else
        {
            SetAlpha(.7f);
        }
    }

    private void DisplayNeededRessources()
    {
        if (ressourcesNeededInstantiated)
        {
            return;
        }

        foreach (var t in upgradeSkill.UpgradeSkillNeedsList)
        {
            if (t.evolveStateNeed == tileList.GetTile(localPos).currentEvolveState)
            {
                foreach (var itemNeeded in t.needsToEvolve)
                {
                    GameObject itemNeedPrefabInstantiate = Instantiate(itemNeedPrefab, needList.transform);
                    itemNeedPrefabInstantiate.GetComponent<Image>().sprite =
                        itemNeeded.ressourceItemNeeded.itemSprite;
                    itemNeedPrefabInstantiate.GetComponentInChildren<TextMeshProUGUI>().text =
                        itemNeeded.ressourceAmountNeeded.ToString()+"x";
                }

                ressourcesNeededInstantiated = true;
                return;
            }
        }
    }


    private void DestroyNeededRessources()
    {
        foreach (Transform t in needList.transform)
        {
            GameObject.Destroy(t.gameObject);
        }

        ressourcesNeededInstantiated = false;
    }



    private void NeedsVerification()
    {
        switch (upgradeSkill.skillType)
        {
            case SkillType.MoreVillagers:
            {
                if (tileList.GetTile(localPos).numberOfVillagerInHouse >= upgradeSkill
                        .UpgradeSkillNeedsList[tileList.GetTile(localPos).currentEvolveState].maxAmountSkill)
                {
                    canUpgradeSkill = false;
                    return;
                }

                break;
            }
            case SkillType.IronBullet:
            {
                if (tileList.GetTile(localPos).numberOfIronBullet >= upgradeSkill
                        .UpgradeSkillNeedsList[tileList.GetTile(localPos).currentEvolveState].maxAmountSkill)
                {
                    canUpgradeSkill = false;
                    return;
                }
                break;

            }
        }



        foreach (var t in upgradeSkill.UpgradeSkillNeedsList)
        {
            if (t.evolveStateNeed == tileList.GetTile(localPos).currentEvolveState)
            {
                int totalRessourceNeeded = t.needsToEvolve.Count;
                int totalRessourceOwned = 0;
                List<UpgradeSkill.Needs> needsToEvolve = t.needsToEvolve;
        
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
                    canUpgradeSkill = true;
                }
                else
                {
                    canUpgradeSkill = false;
                }

                return;
            }
        }
    }
    
    
    private void SetAlpha(float alpha)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha); ;
        Image[] images = GetComponentsInChildren<Image>(true); 
        foreach (Image img in images)
        {
            if (img.gameObject.name != "ButtonToSkillUpgrade")
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
    
    void PlayInteractionAnimation()
    {
         gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(Vector3.one, 0.3f)
            .SetEase(Ease.OutBack,1f); // Effet de rebond smooth
    }
}

