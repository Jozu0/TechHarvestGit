using UnityEngine;




[CreateAssetMenu(fileName = "RessourceItem", menuName = "Scriptable Objects/RessourceItem")]
public class RessourceItem : ScriptableObject
{
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public int ressourceAmount;
    [SerializeField] public RessourceType ressourceType;
    [SerializeField] public ToolType toolType;
}
