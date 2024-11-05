using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float moveSpeed = 5f;
    private Vector2 movementDirection;

    private Rigidbody2D rb;
    private Collider2D attackCollider;
    public ToolPosition toolPosition;
    private Transform toolImageParent;
    public GameObject bulletPrefab;
    public GameObject gunTool;
 


    private void Update()
    {
        Item item = InventoryManager.Instance.GetSelectedItem(false);
        if (item != null)
        {

            CheckTool(item.name);
        }
    }

    private void CheckTool(string toolName)
    {
        foreach (Transform toolImage in toolImageParent)
        {
            if (toolImage.name == toolName)
            {
                toolPosition = toolImage.GetComponent<ToolPosition>();
            }
        }
    }


    private void Awake()
    {
        toolImageParent = transform.Find("Items");

        rb = GetComponent<Rigidbody2D>();
        
        // Enable interpolation for smoother movement
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Start()
    {
        attackCollider = GameObject.FindGameObjectWithTag("PlayerAttack").GetComponent<Collider2D>();
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

    public void MeleeAttack()
    {
        toolPosition.SwordAttack(100);


        if (attackCollider.GetComponent<AttackCollider>().inHitRange != false && attackCollider.GetComponent<AttackCollider>().enemyInRange != null)
        {
            Item Weapon = InventoryManager.Instance.GetSelectedItem(false);
            attackCollider.GetComponent<AttackCollider>().enemyInRange.GetComponent<Monster>().TakeDamage(Weapon.damage);
            Debug.Log(Weapon.damage);
        }

    }
    public void GunAttack(Item gun)
    {
        Instantiate(bulletPrefab, gunTool.transform);
    }
    public void Heal(Item tool)
    {
        InventoryManager.Instance.GetSelectedItem(true);
        Player.instance.Heal(tool.health);
    }

    public void DigGrave()
    {
        PlayerAnimation playerAnimation = GetComponent<PlayerAnimation>();
        playerAnimation.DigGrave(movementDirection);
        GraveManager.instance.DigGrave();
        
    }

    public void SpeedBoost(int value)
    {
        InventoryManager.Instance.GetSelectedItem(true);
        int boost = value;
        StartCoroutine(Speed(boost));
       
    }
    private IEnumerator Speed(int value)
    {
        
        moveSpeed += value;
        yield return new WaitForSeconds(10);
        moveSpeed -= value;
    }
}
