using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 moveDirection;
    [SerializeField] private float delayMoveCam;
    private float timePassed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveCam();
    }

    public void SetMoveDirection(Vector2 moveInput)
    {
        moveDirection = new Vector2(
            moveInput.x == 0 ? 0 : (moveInput.x > 0 ? 1.5f : -1.5f),
            moveInput.y == 0 ? 0 : (moveInput.y > 0 ? 1.5f : -1.5f)
        );    
    }

    void MoveCam()
    {
        if (Time.time >= timePassed)
        {
            timePassed = Time.time + delayMoveCam;
            transform.position = new Vector3(Mathf.Clamp((transform.position.x + moveDirection.x),12,33),
                                             Mathf.Clamp((transform.position.y + moveDirection.y),7.5f,24), -10f);
        }
    }
}
