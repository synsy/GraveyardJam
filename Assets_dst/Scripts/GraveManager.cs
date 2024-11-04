using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveManager : MonoBehaviour
{
    public bool managerDigging = false;
    public List<Grave> graves = new List<Grave>();
    
    public static GraveManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Grave"))
            {
                graves.Add(child.GetComponent<Grave>());
            }
        }
    }

    void Start()
    {
        
    }

    public void DigGrave()
    { 
        for (int i = 0; i < graves.Count; i++)
        {
            if(graves[i].inRange == true)
            {
                graves[i].Dig();
            }
        }
    }

}
