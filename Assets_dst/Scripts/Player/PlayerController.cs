using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movementDirection;

    private Rigidbody2D rb;
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        
        // Enable interpolation for smoother movement
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void SetMovementDirection(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void MovePlayer()
    {
        Vector2 targetPosition = rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }

    public void Attack()
    {
        print("Attacking...");
    }

    public void DigGrave()
    {
        GraveManager.instance.DigGrave();
    }
}
