using System.Collections.Generic;
using UnityEngine;


public enum UIMenuState {Option, Menu, RessourceManagement, BuildManagement, UpgradeManagement}


public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private UIMenuState currentMenuState;
    private UIMenuState previousMenuState;
    
    [SerializeField] public GameObject UIOptionsGameObject, UIMenuGameObject, UIRessourceManagementGameObject, UIBuildManagementGameObject, UIUpgradeManagementGameObject;
    [SerializeField] private List<GameObject> UIList = new List<GameObject>();
    
    
    
    void Start()
    {
        currentMenuState = UIMenuState.RessourceManagement;
        previousMenuState = currentMenuState;
        AddToUIList();
    }

    // Update is called once per frame
    void Update()
    {
        if (previousMenuState != currentMenuState)
        {
            switch (currentMenuState)
            {
                case UIMenuState.Option:
                    ChangeUI("Option Panel");
                    previousMenuState = currentMenuState;
                    break;
                case UIMenuState.Menu:
                    ChangeUI("Menu Panel");
                    previousMenuState = currentMenuState;
                    break;
                case UIMenuState.RessourceManagement:
                    ChangeUI("Ressource Panel");
                    previousMenuState = currentMenuState;
                    break;
                case UIMenuState.BuildManagement:
                    ChangeUI("Build Panel");
                    previousMenuState = currentMenuState;
                    break;
                case UIMenuState.UpgradeManagement:
                    ChangeUI("Upgrade Panel");
                    previousMenuState = currentMenuState;
                    break;
                default:
                    break;
            }
        }
       
    }

    

    private void ChangeUI(string UIName)
    {
        foreach (GameObject ui in UIList)
        {
            if (ui.name == UIName)
            {
                ui.SetActive(true);
            }
            else
            {
                ui.SetActive(false);
            }
        }
    }

    private void AddToUIList()
    {
        UIList.Add(UIOptionsGameObject);
        UIList.Add(UIMenuGameObject);
        UIList.Add(UIRessourceManagementGameObject);
        UIList.Add(UIBuildManagementGameObject);
        UIList.Add(UIUpgradeManagementGameObject);
    }
    
    
    
    
    
    
    public void ChangeCurrentToOption()
    {
        SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.buttonClickFX);
        currentMenuState = UIMenuState.Option;
    }
    public void ChangeCurrentToMenu()
    {
        SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.buttonClickFX);
        currentMenuState = UIMenuState.Menu;
    }
    public void ChangeCurrentToRessourceManagement()
    {
        SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.buttonClickFX);
        currentMenuState = UIMenuState.RessourceManagement;
    }
    public void ChangeCurrentToUpgradeManagement()
    {
        SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.buttonClickFX);

        currentMenuState = UIMenuState.UpgradeManagement;
    }
    public void ChangeCurrentToBuildManagement()
    {
        SoundFXManager.Instance.PlaySfx(SoundFXManager.Instance.buttonClickFX);
        currentMenuState = UIMenuState.BuildManagement;
    }
}
