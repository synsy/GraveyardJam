using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CardPrefabScript : MonoBehaviour
{
    public Image image;
    public Card item;
    public Sprite[] cardsBack;
    private Sprite sprite;

    public void InitialiseCard(Card card)
    {
        item = card;
        image.sprite = card.image;
    }
    public void HideCard()
    {
        sprite = image.sprite;
        int random = Random.Range(0, 8);
        image.sprite = cardsBack[random];
    }
    public void ShowCard()
    {
        image.sprite = sprite;
    }
}
