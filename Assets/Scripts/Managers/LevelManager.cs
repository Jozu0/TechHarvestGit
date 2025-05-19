using UnityEngine;

public enum Levels
{
    Field,
    Cave,
    Ocean
};

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Levels currentLevel;
    
    void Start()
    {
        currentLevel = Levels.Field;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
