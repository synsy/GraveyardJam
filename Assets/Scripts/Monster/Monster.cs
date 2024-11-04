using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterAnimation))]
[RequireComponent(typeof(MonsterController))]
[RequireComponent(typeof(Rigidbody2D))]

public class Monster : MonoBehaviour
{
    public Vector3 spawnPosition;
    public int health { private set; get; } = 50;
    public int damage = 10;
    public float attackDelay = 3f;
    private int difficultyScale = 50;
    private int damageScale = 10;
    public bool canMove = true;

    void Awake()
    {

    }

    void Start()
    {
        SetMonsterDifficulty();
    }

    public void OnColliderEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(100);
        }
    }
    public void TakeDamage(int damage)
    {
        print("Monster took damage: " + health + " health remaining.");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // TODO - Add death logic
        Destroy(gameObject);
    }

    private void SetMonsterDifficulty()
    {
        health += GameManager.instance.currentDay * difficultyScale;
        damage += GameManager.instance.currentDay * (difficultyScale / damageScale);
    }
}
