using UnityEngine;

public class GridPlacementSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public bool isBuildingSelected;
    [SerializeField] public BuildingsInWorld buildingsSelected;
    [SerializeField] public Sprite buildingSpriteSelected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buildingsSelected !=null &&  buildingSpriteSelected == null)
        {
            buildingSpriteSelected = buildingsSelected.buildingSpriteList[buildingsSelected.evolveState].spritesPerSkill[0];
        }
            
    }

    public void NoMoreBuildings()
    {
        isBuildingSelected = false;
        buildingSpriteSelected = null;
        buildingsSelected = null;
        EventManager.TriggerEvent(GameEventType.NoMoreBuildingSelected, null, 0f);
    }
}
