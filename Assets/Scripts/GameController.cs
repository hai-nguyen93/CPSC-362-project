using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<string> deck;
    public int currentTopDeck = 0;

    public Sprite[] cardSprites;
    public TableCards table;

    [Header("Players Hands")]
    public Hand[] players;

    ///////////////////////////
    /// Game Functions
    ///////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        GenerateDeck();
        players[0].AddCardToHand(deck[currentTopDeck]);
        ++currentTopDeck;
        players[0].AddCardToHand(deck[currentTopDeck]);
        ++currentTopDeck;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("aaaaa");
        }
    }

    // Init deck
    public void GenerateDeck()
    {
        string[] suits = { "S", "C", "D", "H" };
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        foreach (string s in suits){
            foreach (string v in values)
            {
                deck.Add(s + v);
            }
        }
        Shuffle();
    }

    // Shuffle the deck
    public void Shuffle()
    {
        for (int i = 0; i < 52; ++i)
        {
            int j = Random.Range(0, 52);
            if (i != j)
            {
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
    }

    public Sprite GetCardSprite(string card)
    {
        for (int i = 0; i < cardSprites.Length; ++i)
        {
            if (card == cardSprites[i].name)
                return cardSprites[i];
        }
        return null;
    }

    // Add card from deck to table
    public void DealCard()
    {
        if (table.Size() < 3)
        {
            for (int i = 0; i < 3; ++i)
            {
                table.AddCard(deck[currentTopDeck]);
                players[0].AddCardCopy(deck[currentTopDeck]);
                ++currentTopDeck;
            }
        }
        else if (table.Size() < 5)
        {
            table.AddCard(deck[currentTopDeck]);
            players[0].AddCardCopy(deck[currentTopDeck]);
            ++currentTopDeck;
        }
    }

    /////////////////////////////
    /// Misc. functions
    ////////////////////////////
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    
    public void Print()
    {
        foreach (string c in deck)
        {
            Debug.Log(c);
        }
    }
}
