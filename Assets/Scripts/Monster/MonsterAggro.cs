using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAggro : MonoBehaviour
{
    private MonsterController monsterController;

    private void Awake()
    {
        monsterController = GetComponentInParent<MonsterController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            monsterController.currentState = MonsterState.Chasing;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox") && monsterController.currentState == MonsterState.Chasing)
        {
            monsterController.currentState = MonsterState.Tracking;
        }

        if (other.CompareTag("PlayerHitBox") && monsterController.currentState == MonsterState.Attacking)
        {
            monsterController.currentState = MonsterState.Chasing;
        }
    }
}

