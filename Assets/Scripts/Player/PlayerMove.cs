using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;
    private float currentMoveSpeed;
    
    [SerializeField] private float normalMoveSpeed;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentMoveSpeed = normalMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetMoveDirection(Vector2 moveInput)
    {
        moveDirection = moveInput;
    }
    
    
     private void Move()
    {
        if (rb2D)
        {
            rb2D.linearVelocity = moveDirection * currentMoveSpeed;
        }

    }

    void Animate()
    {
        
    }

   }
