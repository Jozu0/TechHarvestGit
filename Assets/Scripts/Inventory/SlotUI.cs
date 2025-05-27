using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public RessourceItem actualRessourceItem;
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI amountText;
    [SerializeField] public int ressourceAmount;
    [SerializeField] public GlobalRessourceList globalRessourceList;
    [SerializeField] public int amountToAdd;
     public StatisticData statisticData;
     
     
     void Start()
    {
        image = GetComponent<Image>();
        ressourceAmount = 0;
        if (statisticData.fortuneBoost == 0)
        {
            statisticData.fortuneBoost = amountToAdd;
        }
        amountToAdd = statisticData.fortuneBoost;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewItem(RessourceItem ressourceItem)
    {
        actualRessourceItem = ressourceItem;
        if (ToolManager.Instance.HasTool(ressourceItem.toolType))
        {
            AddMoreItem();
            gameObject.SetActive(true);
        }
        
    }

    public void AddMoreItem()
    {
        ressourceAmount+=amountToAdd;
        RefreshInventory(actualRessourceItem.itemSprite, ressourceAmount);
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
            gameObject.SetActive(true);
        }
    }
}
