using UnityEngine;

[CreateAssetMenu(fileName = "StatisticData", menuName = "Scriptable Objects/StatisticData")]

[System.Serializable]
public class StatisticData : ScriptableObject
{
    
    public int fortuneBoost;
    public float movementSpeed;
    public float bulletSpeed;
    public float bulletRate;
    public float bulletDamage;
    public int totalVillagers;

    void Awake()
    {
        bulletRate=GetBulletDelay(totalVillagers);
    }
    
    public float GetBulletDelay(int citizenCount, int maxCitizens = 152)
    {
        citizenCount = Mathf.Clamp(citizenCount, 0, maxCitizens);

        float baseDelay = 0.8f;
        float minDelay = 0.15f;

        // Progression non linéaire avec exponentielle inverse
        float t = (float)citizenCount / maxCitizens;

        // Courbe ease-out : rapide au début, plus lent ensuite
        float curvedT = 1 - Mathf.Pow(1 - t, 2); // équivalent à "easeOutQuad"

        float delay = Mathf.Lerp(baseDelay, minDelay, curvedT);

        return delay;
    }

    
}

