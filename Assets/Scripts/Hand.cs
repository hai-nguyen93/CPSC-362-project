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
    public int bank;
    public int lastBet; //last amount bet
    protected bool canBet = true;

    // Start is called before the first frame update
    void Start()
    {
        handTypeText.enabled = false;
        bank = 10000;
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
        CheckHand();  
    }

    public void CheckHand()
    {
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
        for(int i = 0; i <= cards.Count - 5; ++i)
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
        if (cards.Count < 5) return false;

        bool has3OfAKind = false;
        int tempValue = -1;
        bool hasPair = false;

        // Look for a Three of a kind
        for (int i = 0; i <= cards.Count - 3; ++i)
        {
            if (cards[0 + i].GetValue() == cards[1 + i].GetValue() && cards[1 + i].GetValue() == cards[2 + i].GetValue())
            {
                has3OfAKind = true;
                tempValue = cards[i].GetValue();
            }
        }

        // Look for a pair, different value from Three of a kind
        for (int i = 0; i <= cards.Count - 2; ++i)
        {
            if (cards[i].GetValue() == cards[1 + i].GetValue() && cards[i].GetValue() != tempValue)
            {
                hasPair = true;
            }
        }

        if (has3OfAKind && hasPair)
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

    ///////////////////
    ///Hand Action
    //////////////////
    
    public void Call()
    {
        //grab last amount bet in, add to bank.
        bank = lastBet + bank;
        Debug.Log("Call");
        // do something
    }

    public void Fold()
    {
        //skip turn of current player
        FindObjectOfType<GameController>().EndPlayerTurn();
        // after this statement, remove player/cards from hand, will add later.
        Debug.Log("Fold");
        // do something
    }

    public void Raise(int raiseAmount)
    {
        //need to implement input from user for amount to raise, add later.
        //(Luc commented this out) int raise = 0;
        //set lastBet equal to amount raised.
        if(raiseAmount < lastBet)
        {
            //tell user to input bet greater than last bet placed in.
        }
        else
        {
            lastBet = raiseAmount;
            bank -= raiseAmount;
            //(Luc commented this out) int raiseAmount = raise + bank;
            //Luc : raiseAmount will be sent from playerController. For AI you can just random generate a reasonable bet 
        }
        Debug.Log("Raise");
        // do something
    }

    public void Check()
    {
        //set betting for current player to false, end turn
        canBet = false;
        FindObjectOfType<GameController>().EndPlayerTurn();
        Debug.Log("Check");
    }
}
