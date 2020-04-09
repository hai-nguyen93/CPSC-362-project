using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HandType { Folded, HighCard, Pair, TwoPairs, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush}

public class Hand : MonoBehaviour
{
    public List<Card> cards;
    public Image card1;
    public Image card2;
    public HandType playerHand;
    public Text handTypeText;
    public int bank;
    public GameObject playerBankText;
    //public int lastBet; //last amount bet => moved to GameController
    protected bool canBet = true;
    public bool isFolded = false;
    public bool acted = false;
    public int totalBet = 0;

    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        ClearHand();
        bank = 10000;
        UpdateBank(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearHand()
    {
        totalBet = 0;
        isFolded = false;
        acted = false;
        cards.Clear();
        card1.enabled = false;
        card2.enabled = false;
        handTypeText.enabled = false;

        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ResetButtons();
        }
    }

    // Add card to hand from deck
    public void AddCardToHand(string card)
    {
        Card c = new Card(card);
        c.sprite = gc.GetCardSprite(c.name);
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
        if (isFolded)
            return HandType.Folded;
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
        for(int i = 0; i <=cards.Count - 5; ++i)
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
        if (counter >= 2)
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
        //bank = lastBet + bank;
        Raise(gc.lastBet - totalBet);
        acted = true;
        Debug.Log("Call");
        // do something
    }

    public void Fold()
    {
        isFolded = true;
        acted = true;
        playerHand = HandType.Folded;
        //skip turn of current player
        //gc.EndPlayerTurn(); -> player turn ended in PlayerController

        // after this statement, remove player/cards from hand, will add later.
        card1.enabled = false;
        card2.enabled = false;
        gc.currentPlayers--;
        Debug.Log("Fold");
        // do something
    }

    public void Raise(int raiseAmount)
    {
        //need to implement input from user for amount to raise, add later.
        //(Luc commented this out) int raise = 0;
        //set lastBet equal to amount raised.
        //if(raiseAmount < gc.lastBet)
        //{
            //tell user to input bet greater than last bet placed in.
        //}
        //else
        //{
            totalBet += raiseAmount;
            gc.lastBet = totalBet;
            //gc.lastBet = raiseAmount;
            UpdateBank(-raiseAmount);
            //(Luc commented this out) int raiseAmount = raise + bank;
            //Luc : raiseAmount will be sent from playerController. For AI you can just random generate a reasonable bet 

            // add $ to potTotal
            gc.UpdatePot(raiseAmount);
        //}
        acted = true;
        Debug.Log("Raise");
        // do something
    }

    public void Check()
    {
        //set betting for current player to false, end turn
        canBet = false;
        acted = true;
        //gc.EndPlayerTurn(); -> player turn ended in Playercontroller
        Debug.Log("Check");
    }

    // change bank amount, positive int for increase, negative int for decrease
    public void UpdateBank(int amount)
    {
        bank += amount;
        playerBankText.GetComponent<Text>().text = "$" + bank;
    }
}
