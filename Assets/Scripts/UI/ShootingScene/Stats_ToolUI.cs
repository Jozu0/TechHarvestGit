using System;
using TMPro;
using UnityEngine;

public class Stats_ToolUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public StatisticData statisticData;

    [SerializeField] private TextMeshProUGUI moveSpeedText; 
    [SerializeField] private TextMeshProUGUI shootingSpeedText; 
    [SerializeField] private TextMeshProUGUI shootDelayText; 
    [SerializeField] private TextMeshProUGUI ressourceFortuneText; 
    [SerializeField] private TextMeshProUGUI shootDamageText; 

    [SerializeField] private GameObject toolList;
    void Start()
    {
        InitializeUITexts();
        UpdateToolsOwned();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateToolsOwned()
    {
        foreach (Transform child in toolList.transform)
        {
            GameObject tool = child.gameObject;
            if (Enum.TryParse(tool.name, out ToolType parsedToolType))
            {
                if (ToolManager.Instance.HasTool(parsedToolType))
                    tool.SetActive(true);
            }
        }
    }
    
    
    private void InitializeUITexts()
    {
        moveSpeedText.text = " : x"+statisticData.movementSpeed.ToString();
        shootingSpeedText.text = " : x"+statisticData.bulletSpeed.ToString();
        shootDelayText.text = " : x"+statisticData.bulletRate.ToString();
        ressourceFortuneText.text = " : x"+statisticData.fortuneBoost.ToString();
        shootDamageText.text = " : x"+statisticData.bulletDamage.ToString();

    }
}
