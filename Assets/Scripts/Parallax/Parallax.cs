using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float scrollSpeed = 0.5f;
    [SerializeField] float ressourcesSpeed;
    private Renderer rend;
    private float spriteHeightUnits;

    void Start()
    {
        rend = GetComponent<Renderer>();
        spriteHeightUnits = transform.localScale.y;
        scrollSpeed = ressourcesSpeed/spriteHeightUnits;

    }

    private void Update()
    {
        rend.material.mainTextureOffset += new Vector2(0, scrollSpeed*Time.deltaTime);
    }
  
}
