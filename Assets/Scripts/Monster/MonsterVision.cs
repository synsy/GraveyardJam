using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVision : MonoBehaviour
{
    private MonsterController monsterController;

    private void Awake()
    {
        monsterController = GetComponentInParent<MonsterController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            monsterController.SetPlayer(other.transform);
            monsterController.currentState = MonsterState.Tracking;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && monsterController.currentState == MonsterState.Tracking)
        {
            monsterController.currentState = MonsterState.Idle;
        }
    }
}

