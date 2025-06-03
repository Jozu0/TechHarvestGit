using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    None, 
    Shears,
    Hammer, 
    Knife,
    Hoe,
    FishingRod
}


public class ToolManager : MonoBehaviour
{
    public static ToolManager Instance;

    private Dictionary<ToolType, bool> ownedTools = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Init tous à false
        foreach (ToolType tool in System.Enum.GetValues(typeof(ToolType)))
        {
            if (!ownedTools.ContainsKey(tool))
                ownedTools.Add(tool, false);
        }
    }


    void Start()
    {
        ownedTools[ToolType.None] = true;
    }
    public void AddTool(ToolType tool)
    {
        ownedTools[tool] = true;
    }

    public bool HasTool(ToolType tool)
    {
        return ownedTools.ContainsKey(tool) && ownedTools[tool];
    }
}