using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GraveManager : MonoBehaviour
{
    public bool managerDigging = false;
    public List<Grave> graves = new List<Grave>();
    private bool[] graveOpened;
    
    public static GraveManager instance;

    private void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            if(InventoryManager.Instance.GetSelectedItem(false) == null)
            {
                return;
            }
            Item tool = InventoryManager.Instance.GetSelectedItem(false);
            if (tool.itemName == "Shovel" || tool.itemName == "BetterShovel" || tool.itemName == "AmazingShovel")
            {
                DigGrave();
            }
            
        }
       
    }

    void Awake()
    {
        graveOpened = new bool[10];
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
           
                
                if (graves[i].inRange == true)
                {

                    graves[i].Dig();
                    
                }
            
           
        }
    }

}
