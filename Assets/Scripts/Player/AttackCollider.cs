using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool inHitRange = false;
    public Collider2D enemyInRange = null;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            inHitRange = true;
            enemyInRange = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            inHitRange = false;
            enemyInRange = null;
        }
    }
}
