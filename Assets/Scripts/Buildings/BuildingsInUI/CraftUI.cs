using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TileList tileList;
    [SerializeField] public Vector2Int localPos;
    [SerializeField] public Craft craft;
    private bool canCraft = false;
    [SerializeField] private GlobalRessourceList globalRessourceList;
    [SerializeField] private Image craftSprite;
    [SerializeField] private TextMeshProUGUI craftNameText;
    [SerializeField] private GameObject needList;
    [SerializeField] private GameObject itemNeedPrefab; 
    [SerializeField] public ObjectOnGrid objectOnGrid;
    private bool ressourcesNeededInstantiated = false;

    void Start()
    {
        UpdateSkillUI();
    }

    public void ButtonCraftPressed()
    {
        UpdateSkillUI();
        if (canCraft)
        {
            SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.craftingButtonFX);
            UpdateGlobalRessources();
            UpdateSkillUI();
            PlayInteractionAnimation();
        }
        EventManager.TriggerEvent(GameEventType.AddOrDeleteRessourceInUI, null, 0);
    }


    private void UpdateGlobalRessources()
    {
        foreach (var itemNeeded in craft.craftItem.needsToCraft)
        {
            globalRessourceList.DeleteRessource(itemNeeded.ressourceItemNeeded.ressourceType,
                        itemNeeded.ressourceAmountNeeded);
        }
        globalRessourceList.AddRessource(craft.craftItem.itemCrafted.ressourceType,craft.craftItem.itemCrafted,craft.craftItem.amountToCraft);
    }


    private void UpdateSkillUI()
    {
        craftSprite.sprite = craft.craftItem.craftSprite;
        craftNameText.text = craft.craftItem.craftName;
        NeedsVerification();
        DisplayNeededRessources();
        if (canCraft)
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
        foreach (var itemNeeded in  craft.craftItem.needsToCraft)
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
        int totalRessourceNeeded = craft.craftItem.needsToCraft.Count; 
        int totalRessourceOwned = 0;
        List<Craft.Needs> needsToEvolve = craft.craftItem.needsToCraft;
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
            canCraft = true;
        }
        else
        {
            canCraft = false;
        }
        return;
    }
    
    private void SetAlpha(float alpha)
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha); ;
        Image[] images = GetComponentsInChildren<Image>(true); 
        foreach (Image img in images)
        {
            if (img.gameObject.name != "ButtonToCraft")
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

