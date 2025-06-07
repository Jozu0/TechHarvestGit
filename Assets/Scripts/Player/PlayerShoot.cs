using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private BulletPool bulletPool;
    private float cooldown;
    [SerializeField] private float currentTime;
    [SerializeField] private int bulletDamage;
    [SerializeField] private GameObject activeBulletQueue;
    public StatisticData statisticData;
    void Start()
    {
        activeBulletQueue = GameObject.FindGameObjectWithTag("PooledBullet");
        cooldown = statisticData.bulletRate;
        cooldown = 0.4f;
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
