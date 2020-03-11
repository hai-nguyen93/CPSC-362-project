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
    public Text handTypeText;

    // Start is called before the first frame update
    void Start()
    {
        handTypeText.enabled = false;
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
        playerHand = CalculateHand();
        handTypeText.text = playerHand.ToString();
        handTypeText.enabled = true;
    }

    public HandType CalculateHand()
    {
        sortHand();
        if (isRoyalFlush())
            return HandType.RoyalFlush;
        else if (isStraightFlush())
            return HandType.StraightFlush;
        else if (isFourOfAKind())
            return HandType.FourOfAKind;
        else if (isFullHouse())
            return HandType.FullHouse;
        else if (isFlush())
            return HandType.Flush;
        else if (isStraight())
            return HandType.Straight;
        else if (isThreeOfAKind())
            return HandType.ThreeOfAKind;
        else if (isTwoPair())
            return HandType.TwoPairs;
        else if (isPair())
            return HandType.Pair;
        else
            return HandType.HighCard;
    }

    public bool isRoyalFlush()
    {
        for (int i = 0; i <= cards.Count - 5; ++i) {
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

    public bool isStraightFlush()
    {
        for(int i = 0; i <= cards.Count - 4; ++i)
        {
            if(cards[0 + i].GetValue() == cards[1 + i].GetValue() + 1 && cards[1 + i].GetValue() == cards[2 + i].GetValue() + 1 && cards[2 + i].GetValue() ==
                cards[3 + i].GetValue() + 1 && cards[3 + i].GetValue() == cards[4 + i].GetValue() + 1 && cards[0 + i].GetSuit() == cards[1 + i].GetSuit()
                && cards[0 + i].GetSuit() == cards[2 + i].GetSuit() && cards[0 + i].GetSuit() == cards[3 + i].GetSuit() &&
                cards[0 + i].GetSuit() == cards[4 + i].GetSuit())
            {
                return true;
            }
        }
        return false;
    }

    public bool isFourOfAKind()
    {
        for (int i = 0; i <= cards.Count - 4; ++i)
        {
            if (cards[0 + i].GetValue() == cards[1 + i].GetValue() && cards[1 + i].GetValue() == cards[2 + i].GetValue() &&
                cards[2 + i].GetValue() == cards[3 + i].GetValue())
            {
                return true;
            }
        }
        return false;
    }

    public bool isFullHouse()
    {
        if (cards.Count < 8) return false;

        int counter1 = 0; // for 3 of a kind
        int counter2 = 0; // for pair
        for (int i = 0; i <= cards.Count - 2; ++i)
        {
            Debug.Log(i);
            if(counter1== 0 && cards[0 + i].GetValue() == cards[1 + i].GetValue() && cards[1 + i].GetValue() == cards[2 + i].GetValue())
            {
                counter1++;
                i += 2;
                continue;
            }
            if(cards[0 + i].GetValue() == cards[1 + i].GetValue())
            {
                counter2++;
            }
        }
        if (counter1 == 1 && counter2 > 0)
            return true;
        return false;
    }

    public bool isFlush()
    {
        for (int i = 0; i <= cards.Count - 5; ++i)
        {
           // Suit currentSuit = cards[i].GetSuit();
            int counter = 0;
            for(int j = i+1; j <= cards.Count - 1; j++)
            {
                if (cards[j].GetSuit() == cards[i].GetSuit())
                    counter++;
            }
            if(counter >= 4) {
                return true;
            }
        }

        return false;
    }

    public bool isStraight()
    {
        for(int i = 0; i <=cards.Count - 4; ++i)
        {
            if(cards[0 + i].GetValue() == cards[1 + i].GetValue() + 1 && cards[1 + i].GetValue() == cards[2 + i].GetValue() + 1 && cards[2 + i].GetValue() ==
                cards[3 + i].GetValue() + 1 && cards[3 + i].GetValue() == cards[4 + i].GetValue() + 1)
            {
                return true;
            }
        }
        return false;
    }

    public bool isThreeOfAKind()
    {
        for (int i = 0; i <= cards.Count - 3; ++i)
        {
            if(cards[0 + i].GetValue() == cards[1 + i].GetValue() && cards[1 + i].GetValue() == cards[2 + i].GetValue())
            {
                return true;
            }
        }
        return false;
    }

    public bool isTwoPair()
    {
        int counter = 0;
        for (int i = 0; i <= cards.Count - 2; i++)
        {
            if(cards[i].GetValue() == cards[i+1].GetValue())
            {
                counter++;
            }
        }
        if (counter == 2)
            return true;
        else
            return false;
    }

    public bool isPair()
    {
        for(int i = 0; i <= cards.Count - 2; ++i)
        {
            if(cards[i].GetValue() == cards[1+i].GetValue())
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
                if (cards[i].CompareCard(cards[j])<= 0)
                {
                    temp = cards[i];
                    cards[i] = cards[j];
                    cards[j] = temp;
                }
            }
        }
    }
}
