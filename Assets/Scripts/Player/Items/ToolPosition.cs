
using System.Collections;
using UnityEngine;

public class ToolPosition : MonoBehaviour
{
    public float targetAngle = 90f; // Target angle to rotate to
    public float rotationSpeed = 200f; // Degrees per second
    private float currentAngle = 0f; // Current angle
    private bool isRotating = false;
    public GameObject attackCollider;
    public bool facingLeft; // for the bullet to be sent in right direction

    private void Awake()
    {
        rotationSpeed = 550;
        facingLeft = true;
    }
    private void Update()
    {
        
        if (isRotating)
        {
            RotateTowardsTarget();
        }
        
        
    }

    // start rotation
    public void SwordAttack(float newTargetAngle)
    {
        targetAngle = newTargetAngle;
        currentAngle = 0f; 
        isRotating = true; 
    }

    private void RotateTowardsTarget()
    {

        float step = rotationSpeed * Time.deltaTime;

    
        if (Mathf.Abs(currentAngle + step) >= Mathf.Abs(targetAngle))
        {
            step = targetAngle - currentAngle; 
        }

        // Rotate object
        transform.Rotate(0, 0, step);
        currentAngle += step; 


        if (Mathf.Abs(currentAngle) >= Mathf.Abs(targetAngle))
        {

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0) ; 
            isRotating = false; 

        }
    }
    public void RightHand()
    {
        Vector3 newPosition = new Vector3(0.35f, -0.13f, 0f); 
        SnapToPosition(newPosition);
        transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        attackCollider.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        facingLeft = false;
        

    }
    public void LeftHand()
    {
        Vector3 newPosition = new Vector3(-0.3f, -0.13f, 0f);
        SnapToPosition(newPosition);
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        attackCollider.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        facingLeft = true;
    }
    public void SnapToPosition(Vector3 targetLocalPosition)
    {
        attackCollider.transform.localPosition = targetLocalPosition;
        transform.localPosition = targetLocalPosition;
    }

}