using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    public static Player instance;
    public int health {private set; get;} = 100;
    public int damage {private set; get;} = 30;
    public bool canMove = true;
    public bool invunrable;
    public Text healthText;

    private void Awake()
    {
        invunrable = false;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep SceneManager between scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        
        healthText.text = health.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (!invunrable)
        {
            health -= damage;
            UpdateHealth();
            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
        
    }   

    private void Die()
    {
        if (InventoryManager.Instance.CheckAllSlotsForLifeSaver())
        {
            health = 100;
            UpdateHealth();
            StartCoroutine(InvunrablePeriod());
        }
        else
        {
            canMove = false;
            GameManager.instance.GameOver();
            
        }
        
    }
    private IEnumerator InvunrablePeriod()
    {
        invunrable = true;
        yield return new WaitForSeconds(2f);
        invunrable = false;
    }

    public void Sleep()
    {
        GameManager.instance.AdvanceDay();
        health = 100;
        UpdateHealth();
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
        UpdateHealth();
    }
    public void SetHealth(int amount)
    {
        health = amount;
        UpdateHealth();
    }
    public void HideHealth()
    {
        healthText.gameObject.SetActive(false);

    }
    public void ShowHealth()
    {
        healthText.gameObject.SetActive(true);

    }
}
