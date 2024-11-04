using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    private float timer;
    public Rigidbody2D rb;
    public int speed = 5;
    public GameObject gun;
    public ToolPosition gunPosition;
    public int move;
    Collider2D enemy;
    private void Awake()
    {
        gun = GameObject.Find("Gun");
        gunPosition = gun.GetComponent<ToolPosition>();
        transform.SetParent(transform.root);
        if (gunPosition.facingLeft)
        {
            move = -speed;
        }
        else
        {
            move = speed;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
          
            enemy = other;
            enemy.GetComponent<Monster>().TakeDamage(1000);
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


    void Update()
    {
        rb.velocity = new Vector2(move, 0);
        timer += Time.deltaTime;
        if (timer > 1)
        {
            Destroy(gameObject);
        }

    }
}
