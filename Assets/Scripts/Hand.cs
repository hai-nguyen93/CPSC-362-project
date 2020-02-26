using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HandType { HighCard, Pair, TwoPairs, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush}

public class Hand : MonoBehaviour
{
    public List<Card> cards;
    public Image card1;
    public Image card2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Add card to hand from deck
    public void AddCardToHand(string card)
    {
        Card c = new Card(card);
        c.sprite = FindObjectOfType<GameController>().GetCardSprite(c.name);
        if (cards.Count == 0)
        {
            card1.sprite = c.sprite;
            card1.enabled = true;
        }
        if (cards.Count == 1)
        {
            card2.sprite = c.sprite;
            card2.enabled = true;
        }
        AddCard(c);
    }

    // Get a copy of a card from the table
    public void AddCardCopy(string card)
    {
        AddCard(new Card(card));
    }

    // Add card to List<Card>
    void AddCard(Card c)
    {
        cards.Add(c);      
    }

    public void CalculateHand()
    {
       //sss
    }
}
