using UnityEngine;
using System;
using UnityEngine.UI;

public class RessourceUI : EventListenerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Ressource currentRessource;
    private RessourceBehaviour ressourceBehaviour;
    [SerializeField] private int UICurrentHP;
    [SerializeField] int maxHealth;
    private Slider healthBar;
    [SerializeField] private GameObject bullet;
    protected override (GameEventType, Action<object,float>)[] GetEventBindings()
    {
        return new (GameEventType, Action<object,float>)[]
        {   
            (GameEventType.RessourceHit, OnRessourceHitUI),
            (GameEventType.RessourceDead, OnRessourceDeadUI),
        };
    }
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   new void  OnEnable()
    {
        base.OnEnable();
        OnRessourceInitializationUI();
    }
    private void OnRessourceHitUI(object data,float damage)
    {
        if ((GameObject)data != this.gameObject) return;
        
        healthBar.value -= damage;
    }
    
    
    private void OnRessourceDeadUI(object data, float value)
    {
        
    }

    
    private void OnRessourceInitializationUI()
    {
        healthBar = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Slider>();
        ressourceBehaviour = GetComponent<RessourceBehaviour>();
        maxHealth = ressourceBehaviour.currentRessourceHP;
        UICurrentHP = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

}
