using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public BuyMenuScript buyMenuScript;
    public int maxStack = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    int selectedSlot = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep InventoryManager between scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ChangeSelectedSlot(0);
    }
    //checks if number on keyboard is pressed and chnages selected slot
    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 8)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }
    //changes selected slot
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }


        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    //adds item to hotbar or inventory, first checks for if the object already exists and if it then is stackable otherwise just tries to add it if space
    public bool AddItem(Item item)
    {
        
        if (item != null)
        {
            //checks for stacking and if the max stack is reached
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStack && itemInSlot.item.stackable == true)
                {

                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return true;

                }
            }
            //checks for empty space and returns false if item cant be added
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {

                    SpawnNewItem(item, slot);
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
       
        
    }
    //an additon to additem. basically just spawns the added item
    void SpawnNewItem(Item item, InventorySlot slot)
    {

        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);

    }

    //get the item in hand. call this when using items or to see whats equppied.
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                    
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
            
        }

        return null;
    }

    //this checks all slots and removes every lootitem
    public void CheckAllSlotsForLoot()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Item loot = itemInSlot.item;

                if (loot.isLoot)
                {
                    buyMenuScript.SellLootItem(loot);
                    Destroy(itemInSlot.gameObject);
                }
            }






        }
    }
    public bool CheckAllSlotsForLifeSaver()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Item item = itemInSlot.item;

                if (item.isLifeSaver)
                {
                    Destroy(itemInSlot.gameObject);
                
                    return true;
                    
                }
            }

        }
        return false;
    }

    public void ClearAllSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && !itemInSlot.item.unsellable)
            {
                    Destroy(itemInSlot.gameObject);
                
            }






        }
    }
    
}
 