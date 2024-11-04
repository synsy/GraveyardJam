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
        // Set animator parameters to movement direction
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);

        // Set moving parameter to true if there is any movement
        bool isMoving = movementDirection.sqrMagnitude > 0;
        animator.SetBool("isMoving", isMoving);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void DigGrave(Vector2 digDirection)
    {
        animator.SetTrigger("Dig");
        animator.SetFloat("Horizontal", digDirection.x);
        animator.SetFloat("Vertical", digDirection.y);
    }
}
