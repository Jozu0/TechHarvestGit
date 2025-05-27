using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float cooldown;
    [SerializeField] private float currentTime;
    [SerializeField] private int bulletDamage;
    [SerializeField] private GameObject activeBulletQueue;
    public StatisticData statisticData;
    void Start()
    {
        activeBulletQueue = GameObject.FindGameObjectWithTag("PooledBullet");
        if (statisticData.bulletRate == 0)
        {
            statisticData.bulletRate = cooldown;
        }
        cooldown = statisticData.bulletRate;
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
            GameObject spawnedBullet = bulletPool.GetPooledBullet();
            if (spawnedBullet != null)
            {
                spawnedBullet.transform.SetParent(activeBulletQueue.transform);
                spawnedBullet.transform.position = activeBulletQueue.transform.position;
                spawnedBullet.SetActive(true);
            }

        }
    }
}
