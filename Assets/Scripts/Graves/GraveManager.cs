using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GraveManager : MonoBehaviour
{
    public bool managerDigging = false;
    public List<Grave> graves = new List<Grave>();
    private bool[] graveOpened;

    public static GraveManager instance;
    public GameObject grave;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (InventoryManager.Instance.GetSelectedItem(false) == null)
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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }


    }

    public void SetAsChild()
    {
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
    public void SpawnGrave()
    {

        int random = Random.Range(5, 15);

        for (int i = 0; i <= random; i++)
        {
            GameObject newGrave = Instantiate(grave, transform.position, Quaternion.identity);


            newGrave.transform.parent = transform;

            int randomPosX = Random.Range(-7, 16);
            int randomPosY = Random.Range(-10, 16);

            newGrave.transform.position = new Vector3(randomPosX, randomPosY, 0);
        }


        foreach (Transform child in transform)
        {
            if (child.CompareTag("Grave"))
            {
                graves.Add(child.GetComponent<Grave>());
            }
        }
    }


}
