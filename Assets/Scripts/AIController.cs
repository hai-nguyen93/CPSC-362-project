using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Hand hand;
    GameController gc;
    
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
        
    }

    public void HideInfo()
    {

    }
}
