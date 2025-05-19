using System.Collections.Generic;
using UnityEngine;


public class BulletPool : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static BulletPool SharedInstance;
    public List<GameObject> pooledBullet;
    public GameObject bulletToPool;
    public int amountToPool;
    

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledBullet = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bulletToPool,gameObject.transform);
            tmp.SetActive(false);
            pooledBullet.Add(tmp);
        }
    }
    public GameObject GetPooledBullet()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledBullet[i].activeInHierarchy)
            {
                return pooledBullet[i];
            }
        }
        return null;
    }
}
