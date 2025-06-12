using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public RessourceItem actualRessourceItem;
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI amountText;
    private int ressourceAmount;
    [SerializeField] public GlobalRessourceList globalRessourceList;
    public StatisticData statisticData;
     
     
     void Awake()
    {
        image = GetComponent<Image>();

    }

    void Start()
    {
        if (actualRessourceItem == null) // Only reset if it's a fresh slot
        {
            ressourceAmount = 0;
        }
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoreItem()
    {
        ressourceAmount += statisticData.fortuneBoost;
        RefreshInventory(actualRessourceItem.itemSprite, ressourceAmount);
    }

    public void AddNewItem(RessourceItem ressourceItem)
    {
        if (!ToolManager.Instance.HasTool(ressourceItem.toolType))
        { 
            return;
        }
        actualRessourceItem = ressourceItem;
        AddMoreItem();
        gameObject.SetActive(true);
    }

    public void RefreshInventory(Sprite itemSprite, int amount)
    {
        
        image.sprite = itemSprite;
        amountText.text = amount.ToString()+"x";
        if (amount < 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            PlayInteractionAnimation();
            gameObject.SetActive(true);
        }
    }
    
    void PlayInteractionAnimation()
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(Vector3.one, 0.3f)
            .SetEase(Ease.OutBack); // Effet de rebond smooth
    }
}
