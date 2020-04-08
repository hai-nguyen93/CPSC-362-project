using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableCards : MonoBehaviour
{
    public List<string> cards;
    public Image[] cardImages;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Image i in cardImages)
            i.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCard(string card)
    {
        if (cards.Count < cardImages.Length)
        {           
            cardImages[cards.Count].sprite = FindObjectOfType<GameController>().GetCardSprite(card);
            cardImages[cards.Count].enabled = true;
            cards.Add(card);
        }
    }

    public void Clear()
    {
        cards.Clear();
        foreach (Image i in cardImages)
        {
            i.enabled = false;
        }
    }

    public int Size()
    {
        return cards.Count;
    }
}
