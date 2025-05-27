using UnityEngine;

[CreateAssetMenu(fileName = "StatisticData", menuName = "Scriptable Objects/StatisticData")]

[System.Serializable]
public class StatisticData : ScriptableObject
{
    
    public int fortuneBoost;
    public float movementSpeed;
    public float bulletSpeed;
    public float bulletRate;
    public int bulletDamage;
}
