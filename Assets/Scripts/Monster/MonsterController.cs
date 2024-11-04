using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterState { Idle, Tracking, Chasing, Attacking }

public class MonsterController : MonoBehaviour
{
    public MonsterState currentState = MonsterState.Idle;
    public float chaseSpeed = 1.33f;
    public float attackRange = 0.3f;
    private Transform player;
    float delay;

    public void Awake()
    {
        delay = GetComponent<Monster>().attackDelay;
    }

    private void Update()
    {
        if (currentState == MonsterState.Tracking)
        {
            TrackPlayer();
        }
        else if (currentState == MonsterState.Chasing)
        {
            ChasePlayer();
        }
        else if (currentState == MonsterState.Attacking)
        {
            // StartCoroutine(AttackPlayerCoroutine());
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    private void TrackPlayer()
    {
        if (player != null)
        {
            // Vector2 direction = player.position - transform.position;
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Adjust needed once we have the monster sprites
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                currentState = MonsterState.Attacking;
                StartCoroutine(AttackPlayerCoroutine());
            }
        }
    }

    IEnumerator AttackPlayerCoroutine()
    {
        
        while (currentState == MonsterState.Attacking)
        {
            if (Vector2.Distance(transform.position, player.position) >= attackRange)
            {
                currentState = MonsterState.Chasing;
                yield return null;
            }
            AttackPlayer();
            yield return new WaitForSeconds(delay);
            
        }
    }
    private void AttackPlayer()
    {
        if(GameManager.instance.currentState == GameManager.GameState.GameOver)
        {
            return;
        }
        this.GetComponent<MonsterAnimation>().PlayAnimation("Attack");
        Player.instance.TakeDamage(this.GetComponent<Monster>().damage);
    }
}

