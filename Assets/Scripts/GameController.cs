using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<string> deck;
    public int currentTopDeck = 0;

    public Sprite[] cardSprites;

    [Header("Players Hands")]
    public Hand player;

    // Start is called before the first frame update
    void Start()
    {
        GenerateDeck();
        player.AddCardToHand(deck[currentTopDeck]);
        ++currentTopDeck;
        player.AddCardToHand(deck[currentTopDeck]);
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

    public void Print()
    {
        foreach (string c in deck)
        {
            Debug.Log(c);
        }
    }

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
    }

    public void Shuffle()
    {

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
}
