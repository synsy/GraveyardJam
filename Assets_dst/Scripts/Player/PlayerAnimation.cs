using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateMovementAnimation(Vector2 movementDirection)
    {
        
    }

    public void Attack()
    {
        
    }
}
