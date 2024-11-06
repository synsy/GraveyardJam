using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class BuyMenuScript : MonoBehaviour
{
    private static BuyMenuScript instance;
    public Button exit, sell, sellAll;
    public InventoryManager inventoryManager;
    public Item[] shopItems; // Array to store shop item

    private Button[] buttons; // Array to store button references
    private UIDocument document;
    public BlackJackManager blackJackManager;
    bool[] owned;
    public BankBalanceScript bankBalanceScript;
    public int money;
    public bool shopReset;




    private void Start()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            Sprite sprite = Resources.Load<Sprite>("GreenButton");
            buttons[i].style.backgroundImage = new StyleBackground(sprite.texture);
        }
        
       


    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep SceneManager between scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
       

        gameObject.SetActive(true);
        document = GetComponent<UIDocument>();

        // Initialize the button array with 12 elements
        buttons = new Button[12];
        owned = new bool[12];
        





    }

    private void UpdateMoneyText()
    {
        blackJackManager.RefreshMoney();
    }
    private void OnEnable()
    {
        UpdateMoneyText();
      
            

        if (shopReset == true)
        {
            ResetShop();
            shopReset = false;
        }
        bankBalanceScript.UpdateBank(money);
        // Retrieve each button from the UI document and store it in the array
        for (int i = 0; i < buttons.Length; i++)
        {

            document = GetComponent<UIDocument>();
            buttons[i] = document.rootVisualElement.Q<Button>($"B{i + 1}");
            int itemIndex = i;
            buttons[i].RegisterCallback<ClickEvent>(evt => BuyItem(itemIndex));


        }
        for (int i = 0; i < owned.Length; i++)
            if (owned[i])
            {
                Sprite sprite = Resources.Load<Sprite>("RedButton");
                buttons[i].style.backgroundImage = new StyleBackground(sprite.texture);
            }
            else
            {
                Sprite sprite = Resources.Load<Sprite>("GreenButton");
                buttons[i].style.backgroundImage = new StyleBackground(sprite.texture);
            }
        //set exit button
        /*exit = document.rootVisualElement.Q<Button>("ExitButton") as Button;
        exit.RegisterCallback<ClickEvent>(ExitMenu);*/
        //set sell and sellall button
        sell = document.rootVisualElement.Q<Button>("SellButton") as Button;
        sell.RegisterCallback<ClickEvent>(SellItem);
        sellAll = document.rootVisualElement.Q<Button>("SellAllButton") as Button;
        sellAll.RegisterCallback<ClickEvent>(SellAll);






    }

    private void ExitMenu(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }

    //buy item using inventory manager add item
    public void BuyItem(int id)
    {

        if (id >= 0 && id < shopItems.Length && !owned[id])
        {
            Item itemToBuy = shopItems[id];
            if (money >= itemToBuy.cost)
            {
                bool addedSuccesfully = inventoryManager.AddItem(itemToBuy);
                if (addedSuccesfully && !itemToBuy.stackable)
                {

                    money -= itemToBuy.cost;
                    Sprite sprite = Resources.Load<Sprite>("RedButton");
                    buttons[id].style.backgroundImage = new StyleBackground(sprite.texture);


                    owned[id] = true;
                    UpdateMoneyText();
                }
                else if (addedSuccesfully)
                {
                    money -= itemToBuy.cost;
                    UpdateMoneyText();

                }
            }



        }

    }

    //sell item using inventorymanager getselecteditem with the bool true that also removes the item
    public void SellItem(ClickEvent evt)
    {

        Item itemToSell = inventoryManager.GetSelectedItem(false);
        if (itemToSell != null && !itemToSell.unsellable)
        {
            inventoryManager.GetSelectedItem(true);
            int itemId = GetItemIDFromShopItems(itemToSell);




            if (itemId >= 0 && !itemToSell.stackable)
            {
                owned[itemId] = false;
                money += itemToSell.value;
                Sprite sprite = Resources.Load<Sprite>("GreenButton");
                buttons[itemId].style.backgroundImage = new StyleBackground(sprite.texture);


                UpdateMoneyText();

            }
            else
            {
                money += itemToSell.value;

                UpdateMoneyText();
            }

        }
    }
    //get id for sellitem
    private int GetItemIDFromShopItems(Item selectedItem)
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] != null && shopItems[i].name == selectedItem.name)
            {
                return i;
            }
        }
        return -1;
    }
    public void SellAll(ClickEvent evt)
    {
        inventoryManager.CheckAllSlotsForLoot();
    }
    public void SellLootItem(Item item)
    {
        money += item.value;
        UpdateMoneyText();
    }
    public void ResetShop()
    {
        for (int i = 0; i < owned.Length; i++)
        {
            owned[i] = false;
        }
    }
    

        
    

}

