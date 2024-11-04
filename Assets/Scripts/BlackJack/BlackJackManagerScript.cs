using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackManager : MonoBehaviour
{
    public static BlackJackManager Instance;
    public CardSlotScript[] cardSlotsDealer;
    public CardSlotScript[] cardSlotsPlayer;
    public Card[] cards;

    public GameObject inventoryItemPrefab;
    public GameObject lose;
    public GameObject win;
    public GameObject push;

    public Button hitButton, standButton, startButton, playAgainButton, doubleButton, betPlusButton, betMinusButton;

    private int dealerTotal, playerTotal, dealerAltTotal, playerAltTotal;
    private int[] dealerCards, playerCards;
    private int random;
    public int bet, lastBet;
    private int hiddenCard = 1;
    public BuyMenuScript buyMenuScript;

    public bool sameCard = false;
    private bool doubleDownLost;
    
    public Text moneyText, betText;
  

    private void Start()
    {
        bet = 10;
        RefreshBet();
        RefreshMoney();
        dealerCards = new int[5];
        playerCards = new int[5];
        
    }
    
    private void OnEnable()
    {
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        lose.SetActive(false);
        win.SetActive(false);
        push.SetActive(false);
        doubleButton.gameObject.SetActive(false);


    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayAgain()
    {
        bet = lastBet;
        betPlusButton.gameObject.SetActive(true);
        betMinusButton.gameObject.SetActive(true);
        ClearAllSlots();
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        lose.SetActive(false);
        win.SetActive(false);
        push.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);
    }
    private void Update()
    {
        
    }
    public void StartBlackJack()
    {
        doubleDownLost = false;
        betPlusButton.gameObject.SetActive(false);
        betMinusButton.gameObject.SetActive(false);
        

        lastBet = bet;

        if (bet <= buyMenuScript.money)
        {
            buyMenuScript.money -= bet;
            RefreshMoney();
            startButton.gameObject.SetActive(false);
            playAgainButton.gameObject.SetActive(false);


            dealerTotal = 0;
            playerTotal = 0;
            dealerAltTotal = 0;
            playerAltTotal = 0;

            StartCoroutine(AddStartCards());
        }

    }
    public IEnumerator AddStartCards()
    {
            for (int i = 0; i < 2; i++)
            {

             yield return new WaitForSeconds(0.6f);
             while (true)
                {
                    sameCard = false;
                    random = Random.Range(0, 52);
                    for (int n = 0; n < playerCards.Length; n++)
                    {
                        if (random == playerCards[n] || random == dealerCards[n])
                        {
                            sameCard = true;
                        }

                    }
                    if (sameCard == false)
                    {
                        break;
                    }
                }


                Card card = cards[random];
                AddCardPlayer(cards[random]);
                playerCards[i] = random;
                playerTotal += card.CardValue;
                if (card.CardValue == 11)
                {

                    playerAltTotal += 1;
                }
                else
                {

                    playerAltTotal += card.CardValue;
                }

            yield return new WaitForSeconds(0.6f);

                while (true)
                {
                    sameCard = false;
                    random = Random.Range(0, 52);
                    for (int n = 0; n < playerCards.Length; n++)
                    {
                        if (random == playerCards[n] || random == dealerCards[n])
                        {
                            sameCard = true;
                        }

                    }
                    if (sameCard == false)
                    {
                        break;
                    }
                }
                card = cards[random];
                AddCardDealer(card);
                dealerCards[i] = random;
                dealerTotal += card.CardValue;
                if (card.CardValue == 11)
                {

                    dealerAltTotal += 1;
                }
                else
                {

                    dealerAltTotal += card.CardValue;
                }




            }


            if (playerTotal == 21)
            {
                PlayerBlackJack();
            }
            HideDealerCard(hiddenCard);

            doubleButton.gameObject.SetActive(true);

            HitOrStand();




        
    }

    public void DoubleDown()
    {
        buyMenuScript.money -= bet;
        RefreshMoney();
        bet += bet;

        PlayerHit();
        if (!doubleDownLost)
        {
            PlayerStand();
        }
        

        doubleButton.gameObject.SetActive(false);

    }

  
    public void HitOrStand()
    {
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
    }

    public void PlayerHit()
    {
        doubleButton.gameObject.SetActive(false);
            
        while (true)
        {
            sameCard = false;
            random = Random.Range(0, 52);
            for (int n = 0; n < playerCards.Length; n++)
            {
                if (random == playerCards[n] || random == dealerCards[n])
                {
                    sameCard = true;
                }

            }
            if (sameCard == false)
            {
                break;
            }
        }
        Card card = cards[random];
        AddCardPlayer(card);
        playerTotal += card.CardValue;
        if(card.CardValue == 11)
        {
            playerAltTotal += 1;
        }
        else
        {
            playerAltTotal += card.CardValue;
        }
        if (playerTotal > 21 && playerAltTotal > 21)
        {
            PlayerLose();
            doubleDownLost = true;
        }
       
    }

    public void PlayerStand()
    {
        doubleButton.gameObject.SetActive(false);
        HideHitStand();
        ShowDealerCard(hiddenCard);
        if (dealerTotal >= 17 && dealerTotal <= 21 || dealerAltTotal >= 17 && dealerAltTotal <= 21)
        {
            CompareTotal();

        }
        else if (dealerTotal > 21 && dealerAltTotal > 21)
        {
            PlayerWin();
        }
        else
        {
            StartCoroutine(DealerHit());
        }
       

    }

    



    public IEnumerator DealerHit()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.6f);
            while (true)
            {
                sameCard = false;
                random = Random.Range(0, 52);
                for (int n = 0; n < playerCards.Length; n++)
                {
                    if (random == playerCards[n] || random == dealerCards[n])
                    {
                        sameCard = true;
                    }

                }
                if (sameCard == false)
                {
                    break;
                }
            }
            Card card = cards[random];
            AddCardDealer(card);
            dealerCards[i + 2] = random;
            dealerTotal += card.CardValue;
            if (card.CardValue == 11)
            {
                dealerAltTotal += 1;
            }
            else
            {
                dealerAltTotal += card.CardValue;
            }

            if (dealerTotal >= 17 && dealerTotal <= 21 || dealerAltTotal >= 17 && dealerAltTotal <= 21)
            {
                CompareTotal();
                break;
            }
            else if (dealerTotal > 21 && dealerAltTotal > 21)
            {
                PlayerWin();
                break;

            }
            
        }
    }
    private void PlayerLose()
    {
        lose.SetActive(true);
        HideHitStand();
        playAgainButton.gameObject.SetActive(true);
    }
    private void PlayerWin()
    {
        win.SetActive(true);
        HideHitStand();
        playAgainButton.gameObject.SetActive(true);
        buyMenuScript.money += bet * 2;
        RefreshMoney();
       
    }
    private void PlayerBlackJack()
    {
        win.SetActive(true);
        HideHitStand();
        playAgainButton.gameObject.SetActive(true);
        buyMenuScript.money += bet * (5/2);
        RefreshMoney();
       
    }
    private void Push()
    {
        HideHitStand();
        push.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        buyMenuScript.money += bet;
        RefreshMoney();

    }
    public void RefreshMoney()
    {
        moneyText.text = buyMenuScript.money.ToString();
        buyMenuScript.bankBalanceScript.UpdateBank(buyMenuScript.money);
    }
    private void HideHitStand()
    {
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
    }
    private void CompareTotal()
    {
        int compareTotalPlayer;
        int compareTotalDealer;
         
        if (playerAltTotal <= 21 && playerTotal <= 21)
        {
            compareTotalPlayer = playerTotal;
        }
        else
        {
            compareTotalPlayer = playerAltTotal;
        }
        if (dealerAltTotal <= 21 && dealerTotal <= 21)
        {
            compareTotalDealer = dealerTotal;
        }
        else
        {
            compareTotalDealer = dealerAltTotal;
        }

        if (compareTotalPlayer > compareTotalDealer)
        {
            PlayerWin();
        }
        else if (compareTotalDealer > compareTotalPlayer)
        {
            PlayerLose();
        }
        else if (compareTotalPlayer == compareTotalDealer)
        {
            Push();
        }
    }
    public bool AddCardDealer(Card card)
    {
        
        if (card != null)
        {
            //checks for stacking and if the max stack is reached
            for (int i = 0; i < cardSlotsDealer.Length; i++)
            {
                CardSlotScript slot = cardSlotsDealer[i];
                CardPrefabScript itemInSlot = slot.GetComponentInChildren<CardPrefabScript>();
                if (itemInSlot == null)
                {

                    SpawnNewCard(card, slot);
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
    public void HideDealerCard (int id)
    {
        CardSlotScript slot = cardSlotsDealer[id];
        CardPrefabScript itemInSlot = slot.GetComponentInChildren<CardPrefabScript>();
        if (itemInSlot != null)
        {
            itemInSlot.HideCard();
        }
       
    }
    public void ShowDealerCard(int id)
    {
        CardSlotScript slot = cardSlotsDealer[id];
        CardPrefabScript itemInSlot = slot.GetComponentInChildren<CardPrefabScript>();
        if (itemInSlot != null)
        {
            itemInSlot.ShowCard();
        }

    }
    public bool AddCardPlayer(Card card)
    {

        if (card != null)
        {
            //checks for stacking and if the max stack is reached
            for (int i = 0; i < cardSlotsPlayer.Length; i++)
            {
                CardSlotScript slot = cardSlotsPlayer[i];
                CardPrefabScript cardPrefab = slot.GetComponentInChildren<CardPrefabScript>();
                if (cardPrefab == null)
                {

                    SpawnNewCard(card, slot);
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

    void SpawnNewCard(Card card, CardSlotScript slot)
    {

        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        CardPrefabScript cardPrefab = newItemGo.GetComponent<CardPrefabScript>();
        cardPrefab.InitialiseCard(card);

    }



    public void ClearAllSlots()
    {
        for (int i = 0; i < cardSlotsDealer.Length; i++)
        {
            CardSlotScript slot = cardSlotsDealer[i];
            CardPrefabScript itemInSlot = slot.GetComponentInChildren<CardPrefabScript>();
            if (itemInSlot != null)
            {
                    Destroy(itemInSlot.gameObject);
                
            }

        }
        for (int i = 0; i < cardSlotsPlayer.Length; i++)
        {
            CardSlotScript slot = cardSlotsPlayer[i];
            CardPrefabScript itemInSlot = slot.GetComponentInChildren<CardPrefabScript>();
            if (itemInSlot != null)
            {
                Destroy(itemInSlot.gameObject);

            }

        }
    }

    public void AddBet()
    { 
        bet += 10;
        RefreshBet();
    }
    public void RemoveBet()
    {
        if (bet >= 20)
        {
            bet -= 10;
        }
        RefreshBet();
    }
    private void RefreshBet()
    {
        betText.text = bet.ToString();
    }
    
}
 
