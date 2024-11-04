using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BuyManager : MonoBehaviour
{
    public Button exit;
    public InventoryManager inventoryManager;
    public Item[] shopItems; // Array to store shop items

    private Button[] buttons; // Array to store button references
    private UIDocument document;
    bool[] owned;

    private void Awake()
    {
        
        document = GetComponent<UIDocument>();

        exit = document.rootVisualElement.Q<Button>("ExitButton") as Button;
        exit.RegisterCallback<ClickEvent>(ExitMenu);


        // Initialize the button array with 12 elements
        buttons = new Button[12];
        owned = new bool[12];

        // Retrieve each button from the UI document and store it in the array
        for (int i = 0; i < buttons.Length; i++)
        {
            // Assuming button names in the UI document are "B1", "B2", ..., "B12"
            buttons[i] = document.rootVisualElement.Q<Button>($"B{i + 1}");

            // Register a callback to each button using its index
            int itemIndex = i; // Capture the current index in a local variable
            buttons[i].RegisterCallback<ClickEvent>(evt => BuyItem(itemIndex));
        }
    }

    private void ExitMenu(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }

    public void BuyItem(int id)
    {
       
        if (id >= 0 && id < shopItems.Length && !owned[id])
        {
            Item itemToBuy = shopItems[id];
            bool addedSuccesfully = inventoryManager.AddItem(itemToBuy);
            if (addedSuccesfully && !itemToBuy.stackable)
            {
         
                buttons[id].style.backgroundImage = new StyleBackground(
                    AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Graphics/redbutton.asset"));
                owned[id] = true;
            }
           
        }
        else
        {
            print("NO");
        }
    }
}
