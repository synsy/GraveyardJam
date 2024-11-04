using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.XR;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;
    private KeyCode digGraveKey = KeyCode.E;  // Key for digging graves

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

    }

    private void Update()
    {
        HandleMovementInput();
        HandleAttackInput();
        HandleDigGraveInput();
    }

    private void HandleMovementInput()
    {
        // Get horizontal and vertical input (WASD / Arrow Keys)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movementDirection = new Vector2(horizontal, vertical).normalized;

        // Pass movement direction to PlayerMovement script
        playerController.SetMovementDirection(movementDirection);
    }

    private void HandleAttackInput()
    {
        // Check if attack button is pressed: Spacebar or Left Mouse Button
        if (Input.GetButtonDown("Fire1"))
        {
            playerController.Attack();
        }
    }

    private void HandleDigGraveInput()
    {
        // Check if dig grave button is pressed: E key
        if (Input.GetKeyDown(digGraveKey))
        {
            playerController.DigGrave();
        }
    }
}

