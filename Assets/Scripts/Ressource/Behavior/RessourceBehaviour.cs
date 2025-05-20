using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class RessourceBehaviour : EventListenerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private List<RessourceLists> ressourcesListBiome1 = new List<RessourceLists>();
    private List<RessourceLists> currentRessourcesList = new List<RessourceLists>(); 
    private Ressource currentRessourceToSpawn;
    private int randomIndexSprite;
    public int currentRessourceHP;
    private RessourceType currentRessourceType;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private GameObject pooledObjects;
    private RessourceManager ressourceManager;
    
    [SerializeField] private float ressourcesSpeed;

    
    protected override (GameEventType, Action<object,float>)[] GetEventBindings()
    {
        return new (GameEventType, Action<object,float>)[]
        {
            (GameEventType.RessourceHit, OnRessourceHitBehaviour),
            (GameEventType.RessourceDead, OnRessourceDeadBehaviour),
        };
    }


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentRessourcesList = ressourcesListBiome1; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        pooledObjects = GameObject.FindGameObjectWithTag("PooledObject");
        ressourceManager = GameObject.FindGameObjectWithTag("RessourceManager").GetComponent<RessourceManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, -ressourcesSpeed);
    }

    new void OnEnable()
    {
        base.OnEnable();
        OnRessourceInitializationBehaviour();
        ToolManager.Instance.AddTool(ToolType.None);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DespawnEra"))
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(pooledObjects.transform);
        }

        if (other.CompareTag("Bullet"))
        {
            float bulletDamage = other.gameObject.GetComponent<BulletBehavior>().bulletDamage;
            EventManager.TriggerEvent(GameEventType.RessourceHit, this.gameObject, bulletDamage);
        }
    }
    
    
    private void OnRessourceHitBehaviour(object data, float damage)
    {
        if (!ReferenceEquals((GameObject)data, this.gameObject)) return;
        if (currentRessourceHP > 1)
        {
            currentRessourceHP-=(int)damage;
        }
        else
        {
            EventManager.TriggerEvent(GameEventType.RessourceDead, this.gameObject);
        }
    }

    private void OnRessourceDeadBehaviour(object data, float damage)
    {
        if ((GameObject)data != this.gameObject) return;
        gameObject.SetActive(false);
        ressourceManager.AddItem(currentRessourceToSpawn);
        Debug.Log(currentRessourceToSpawn);
    }

    private void OnRessourceInitializationBehaviour()
    {
        currentRessourceToSpawn = GetRandomRessource();
        randomIndexSprite = UnityEngine.Random.Range(0,currentRessourceToSpawn.ressourceSprite.Count);
        spriteRenderer.sprite = currentRessourceToSpawn.ressourceSprite[randomIndexSprite];
        currentRessourceHP = currentRessourceToSpawn.HP;
    }

    private Ressource GetRandomRessource()
    { 
        int[] weightedTable = {
            0,0,0,0,0,0,0,0,0,  // 9 -> 45%
            1,1,1,1,1,1,1,      // 7 -> 35%  
            2,2,2,2             // 4 -> 20%
        }; 
        RessourceLists randomRessourceList = currentRessourcesList[weightedTable[Random.Range(0, weightedTable.Length)]];
        int randomIndex = UnityEngine.Random.Range(0, randomRessourceList.RessourceList.Count);
        Ressource ressourceToSpawn = randomRessourceList.RessourceList[randomIndex];
        return ressourceToSpawn;
    }
}
