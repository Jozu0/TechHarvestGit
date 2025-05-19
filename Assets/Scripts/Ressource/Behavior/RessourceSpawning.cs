using System.Collections.Generic;
using UnityEngine;

public class RessourceSpawning : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private List<GameObject> slotList = new List<GameObject>();
    private GameObject spawningEra;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private RessourcePool ressourcePool;
    
    
    void Start()
    {
        spawningEra = GameObject.FindGameObjectWithTag("SpawningEra");
        foreach (Transform slot in spawningEra.transform)
        {
            slotList.Add(slot.gameObject);
        }
    }

    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnRessources),timeToSpawn,timeToSpawn);
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnRessources()
    {
        GameObject spawnedRessource = ressourcePool.GetPooledObject();
        if (spawnedRessource != null)
        {
            GameObject currentSlot = slotList[Random.Range(0, slotList.Count - 1)];
            spawnedRessource.transform.SetParent(currentSlot.transform);
            spawnedRessource.transform.position = currentSlot.transform.position;
            spawnedRessource.SetActive(true);
        }
        
    }
}
