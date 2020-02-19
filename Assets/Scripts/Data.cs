using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit { Error, Spade, Club, Diamond, Heart};

[System.Serializable]
public class Card
{
    public string name;
    public Sprite sprite;
    
    public Card(string _name)
    {
        this.name = _name;      
    }

    public int GetValue()
    {
        char c = name[1];
        switch (c)
        {
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return c - '0';              
            case '1':
                return 10;               
            case 'J':
                return 11;               
            case 'Q':
                return 12;
            case 'K':
                return 13;
            case 'A':
                return 14;
        }
        return -1;
    }

    public Suit GetSuit()
    {
        char c = name[0];
        switch (c)
        {
            case 'S':
                return Suit.Spade;
            case 'C':
                return Suit.Club;
            case 'D':
                return Suit.Diamond;
            case 'H':
                return Suit.Heart;        
        }

        return Suit.Error;
    }
}


