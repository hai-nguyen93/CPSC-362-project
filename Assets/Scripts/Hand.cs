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
    public HandType playerHand;

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

    public HandType CalculateHand()
    {
        sortHand();
        if (isRoyalFlush())
            return HandType.RoyalFlush;
    }

    public bool isRoyalFlush()
    {
        for (int i = 0; i <= cards.Count - 4; ++i) {
            if (cards[0 + i].GetValue() == 14 && cards[1+i].GetValue() == 13 && cards[2+i].GetValue() == 12
                && cards[3+i].GetValue() == 11 && cards[4+i].GetValue() == 10 && cards[0+i].GetSuit() == cards[1+i].GetSuit()
                && cards[0+i].GetSuit() == cards[2+i].GetSuit() && cards[0+i].GetSuit() == cards[3+i].GetSuit() &&
                cards[0+i].GetSuit() == cards[4+i].GetSuit())
            {
                return true;
            }
        }
        return false;
    }



    public void sortHand()
    {
        Card temp;
        for(int i = 0; i < cards.Count; i++)
        {
            for(int j = i+1; j < cards.Count; j++)
            {
                if (cards[i].CompareCard(cards[j]) <= 0)
                {
                    temp = cards[i];
                    cards[i] = cards[j];
                    cards[j] = temp;
                }
            }
        }
    }
}
