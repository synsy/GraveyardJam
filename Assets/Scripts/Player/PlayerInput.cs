using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public  PlayerController playerController;
    private PlayerAnimation playerAnimation;
    private KeyCode useKey = KeyCode.E;  // Key for using things
    public ToolPosition toolPosition;
    public GameObject shop;
    private Transform toolImageParent;
    public AudioManager audioManager;
 

    private float attackCooldown = 0.3f; // Cooldown duration for attack
    private float lastAttackTime = -Mathf.Infinity;
    private float shotCooldown = 0.8f; // Cooldown duration for gun
    private float lastShotTime = -Mathf.Infinity;
    private float consumableCooldown = 10f; // Cooldown duration for consumable
    private float lastConsumableTime = -Mathf.Infinity;

    private void Awake()
    {
        toolImageParent = transform.Find("Items");
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if(Player.instance.canMove)
        {
            HandleMovementInput();
            HandleAttackInput();
            HandleUseInput();
        }
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

    private void HandleMovementInput()
    {
        // Get horizontal and vertical input (WASD / Arrow Keys)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movementDirection = new Vector2(horizontal, vertical).normalized;

        // Pass movement direction to PlayerMovement script
        playerController.SetMovementDirection(movementDirection);
        playerAnimation.UpdateMovementAnimation(movementDirection);

        if (horizontal > 0)
        {
            toolPosition.RightHand();
        }
        if (horizontal < 0)
        {
            toolPosition.LeftHand();
        }

        if (horizontal != 0 || vertical != 0)
        {
            audioManager.Footsteps();
            
        }
    }

    private void HandleAttackInput()
    {
        if (shop.activeSelf == false)
        {
            Item tool = InventoryManager.Instance.GetSelectedItem(false);
            
            if (tool != null && tool.isMelee)
            {
                // Check if attack button is pressed: Spacebar or Left Mouse Button
                if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
                {
                    playerController.MeleeAttack();

                    audioManager.Swing();

                    lastAttackTime = Time.time;
                }
            }
            else if (tool != null && tool.name == "EnergyDrink")
            {
                
                if (Input.GetButtonDown("Fire1"))
                {
                    playerController.SpeedBoost(tool.speedGain);
                }

            }
            else if (tool != null && tool.isConsumable && Player.instance.health != 100)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= lastConsumableTime + consumableCooldown && tool.name != "EnergyDrink")
                {
                    playerController.Heal(tool);
                    lastConsumableTime = Time.time;
                }

            }
            else if(tool != null && tool.isGun)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= lastShotTime + shotCooldown)
                {
                    playerController.GunAttack(tool);
                    lastShotTime = Time.time;
                }
            }
            
        }
        
        
    }

    private void HandleUseInput()
    {
        // Check if dig grave button is pressed: E key
        if (Input.GetKeyDown(useKey))
        {
            
        }
    }
}

