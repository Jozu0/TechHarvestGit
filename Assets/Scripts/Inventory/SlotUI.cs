using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public RessourceItem actualRessourceItem;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] public int ressourceAmount;
    
    void Start()
    {
        image = GetComponent<Image>();
        ressourceAmount = 0;
        gameObject.SetActive(false);
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
        Debug.Log("ajout d un item");
        ressourceAmount++;
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        image.sprite = actualRessourceItem.itemSprite;
        amountText.text = ressourceAmount.ToString()+"x"; 
    }
}
