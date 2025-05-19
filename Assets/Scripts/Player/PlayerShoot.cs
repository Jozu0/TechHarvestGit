using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float cooldown;
    [SerializeField] private float currentTime;
    [SerializeField] private GameObject activeBulletQueue;
    void Start()
    {
        activeBulletQueue = GameObject.FindGameObjectWithTag("PooledBullet");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBullet()
    {
        if (Time.time >= currentTime)
        {
            currentTime = Time.time + cooldown;
            GameObject spawnedRessource = bulletPool.GetPooledBullet();
            if (spawnedRessource != null)
            {
                spawnedRessource.transform.SetParent(activeBulletQueue.transform);
                spawnedRessource.transform.position = activeBulletQueue.transform.position;
                spawnedRessource.SetActive(true);
            }

        }
    }
}
