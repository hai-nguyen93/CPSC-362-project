using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    Hand hand;
    GameController gc;

    public Sprite cardBack;
    Sprite card1Sprite;
    Sprite card2Sprite;
    
    // Start is called before the first frame update
    void Start()
    {      
        hand = GetComponent<Hand>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Act()
    {
        if (hand.totalBet < gc.lastBet)
        {
            if (hand.bank > 0)
            {
                if (hand.bank < gc.lastBet - hand.totalBet)
                {
                    Debug.Log("AI all in");
                    hand.Raise(hand.bank);
                }
                else
                {
                    Debug.Log("AI Call.");
                    hand.Call();
                }
            }
            else
            {
                Debug.Log("AI check - $0 left");
                hand.Check();
            }
        }
        else if (hand.totalBet == gc.lastBet)
        {
            Debug.Log("AI Check.");
            hand.Check();
        }
        gc.EndPlayerTurn();
    }

    public void ShowInfo()
    {
        hand.handTypeText.enabled = true;
        hand.card1.sprite = card1Sprite;
        hand.card2.sprite = card2Sprite;
    }

    public void HideInfo()
    {
        card1Sprite = hand.card1.sprite;
        card2Sprite = hand.card2.sprite;
        hand.card1.sprite = cardBack;
        hand.card2.sprite = cardBack;
        hand.handTypeText.enabled = false;
    }
}
