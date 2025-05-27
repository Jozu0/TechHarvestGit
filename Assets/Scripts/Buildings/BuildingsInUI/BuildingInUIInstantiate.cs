using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingInUIInstantiate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BuildingsInMenuList buildingsInMenuList;
    [SerializeField] private GameObject buildingsPrefabUI;
    public List<BuildingsInUIBehaviour> buildingsInUIBehaviourList = new List<BuildingsInUIBehaviour>();
    void Start()
    {
        buildingsInMenuList.buildingInMenu = buildingsInMenuList.buildingInMenu
            .OrderBy(b => b.buildingType)
            .ToList();
        List<BuildingsInMenu> buildingList = buildingsInMenuList.buildingInMenu;
        foreach (BuildingsInMenu building in buildingList)
        {
            GameObject instantiatePrefab = Instantiate(buildingsPrefabUI, transform);
            instantiatePrefab.GetComponent<BuildingsInUIBehaviour>().buildingsInMenu = building;
            buildingsInUIBehaviourList.Add(instantiatePrefab.GetComponent<BuildingsInUIBehaviour>());
        }
        float newHeight = 59*buildingList.Count;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, newHeight);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
