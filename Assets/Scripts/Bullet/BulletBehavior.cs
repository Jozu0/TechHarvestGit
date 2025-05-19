using UnityEngine;
using System;

public class BulletBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject inactiveBulletQueue;
    private Animator anim;
    private Rigidbody2D rb2D;
    [SerializeField] private bool notExploding;
    [SerializeField] private float bulletSpeed;
    [SerializeField] public float bulletDamage;

    void Start()
    {
       
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        inactiveBulletQueue = GameObject.FindGameObjectWithTag("BulletToPool");
    }


    void OnEnable()
    {
        notExploding = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (notExploding)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, +bulletSpeed );
        }
    }
    
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ressource"))
        {
            notExploding = false;
            rb2D.linearVelocity = Vector2.zero;
            anim.SetTrigger("Explosion");
            //Faire un truc dans la ressource 

        }

        if (other.gameObject.CompareTag("DestroyBulletEra"))
        {
            SetInactiveBullet();
        }
    }


    public void SetInactiveBullet()
    {
        transform.SetParent(inactiveBulletQueue.transform);
        gameObject.SetActive(false);
    }


   
}
