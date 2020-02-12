using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit { Spade, Club, Diamond, Heart};

public class Card
{
    public string name;
    public Sprite sprite;
    
    public Card(string _name)
    {
        this.name = _name;      
    }

    public int getValue()
    {
        return 1;
    }

    public Suit getSuit()
    {
        return Suit.Spade;
    }
}


