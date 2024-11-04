using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{
    private int hours = 0;
    private int minutes = 0;
    public float timer = 0;
    private float interval = 1f;
    public Text clockText;
    public Color color;
    private bool gameEnded;
   
    void Start()
    {
        clockText.color = Color.white;
    }

    private void Awake()
    {
        timer = 0;
        clockText.color = Color.white;
        gameEnded = false;
    }


    void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            minutes++;

            if (minutes >= 60)
            {
                hours++;
                minutes = 0;
            }
            timer = 0;

           
        }

        if (hours >= 6)
        {
            timeEnd();
        }
        updateClock();

        if (hours >= 5)
        {
            clockText.color = color;
        }
        
    }
    public void updateClock()
    {
        clockText.text = $"{hours:00}:{minutes:00}";
    }
    public void timeEnd()
    {
        if (!gameEnded) 
        {
            GameManager.instance.GameOver();
            gameEnded = true;
        }
    }
    
    
}

