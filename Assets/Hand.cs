using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public List<Card> cards;
    public Image card1;
    public Image card2;
    int currentHandSize = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHandSize = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCardToHand(string card)
    {
        Card c = new Card(card);
        c.sprite = FindObjectOfType<GameController>().GetCardSprite(c.name);
        if (currentHandSize == 0)
        {
            card1.sprite = c.sprite;
            card1.enabled = true;
        }
        if (currentHandSize == 1)
        {
            card2.sprite = c.sprite;
            card2.enabled = true;
        }
        ++currentHandSize;
    }
}
